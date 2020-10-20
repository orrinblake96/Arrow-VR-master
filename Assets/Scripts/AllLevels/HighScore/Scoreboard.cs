using System.IO;
using UnityEngine;

namespace AllLevels.HighScore
{
    public class Scoreboard : MonoBehaviour
    {
        [SerializeField] private int maxScoreboardEntries = 5;
        [SerializeField] private Transform highScoresHolderTransform;
        [SerializeField] private GameObject scoreBoardEntryObject;

        [SerializeField] private string savePathName;
        
        [SerializeField] ScoreboardEntryData testData = new ScoreboardEntryData();

        private ScoreboardSaveData savedScores;
        private string SavePath => $"{Application.persistentDataPath}/" + savePathName + ".json";

        private void Start()
        {
            savedScores = GetSavedScores();
            SaveScores(savedScores);
            UpdateUI(savedScores);
        }

        [ContextMenu("Add Test")]
        public void Test()
        {
            AddEntry(testData);
        }

        public void AddEntry(ScoreboardEntryData scoreboardEntryData)
        {
            savedScores = GetSavedScores();

            bool scoreAdded = false;

            for (int i = 0; i < savedScores.highscores.Count; i++)
            {
                if (scoreboardEntryData.entryScore > savedScores.highscores[i].entryScore)
                {
                    savedScores.highscores.Insert(i, scoreboardEntryData);
                    scoreAdded = true;
                    break;
                }
            }

            if (!scoreAdded && savedScores.highscores.Count < maxScoreboardEntries)
            {
                savedScores.highscores.Add(scoreboardEntryData);
            }

            if (savedScores.highscores.Count > maxScoreboardEntries)
            {
                savedScores.highscores.RemoveRange(maxScoreboardEntries, 
                    savedScores.highscores.Count - maxScoreboardEntries);
            }
            
            UpdateUI(savedScores);

            SaveScores(savedScores);
        }

        private void UpdateUI(ScoreboardSaveData scoreboardSaveData)
        {
            foreach (Transform child in highScoresHolderTransform)
            {
                Destroy(child.gameObject);
            }

            foreach (ScoreboardEntryData highscore in savedScores.highscores)
            {
                Instantiate(scoreBoardEntryObject, highScoresHolderTransform).
                    GetComponent<ScoreboardEntryUI>().Initialise(highscore);
            }
        }

        private ScoreboardSaveData GetSavedScores()
        {
            if (!File.Exists(SavePath))
            {
                File.Create(SavePath).Dispose();
                return new ScoreboardSaveData();
            }
            
            using (StreamReader stream = new StreamReader(SavePath))
            {
                string json = stream.ReadToEnd();
                return JsonUtility.FromJson<ScoreboardSaveData>(json);
            }
        }

        private void SaveScores(ScoreboardSaveData scoreboardSaveData)
        {
            using (StreamWriter stream = new StreamWriter(SavePath))
            {
                string json = JsonUtility.ToJson(scoreboardSaveData, true);
                stream.Write(json);
            }
        }
    }
}
