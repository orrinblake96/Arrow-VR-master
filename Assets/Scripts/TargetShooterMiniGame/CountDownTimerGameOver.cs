using System;
using AllLevels.HighScore;
using FMODUnity;
using UnityEngine;

namespace TargetShooterMiniGame
{
    public class CountDownTimerGameOver : MonoBehaviour
    {
        [Header("Hideable/Showable Objects")]
        [SerializeField] private GameObject[] objectsToShow;
        [SerializeField] private GameObject[] objectsToHide;
        
        [Header("Game Over Sounds")]
        [SerializeField] private string winSoundPath;
        [SerializeField] private string loseSoundPath;
        
        private Animator _scorePulsate;
        private static readonly int GameOver = Animator.StringToHash("GameOver");
        
        private ScoreboardEntryData _entryData = new ScoreboardEntryData();
        private Scoreboard _highscoreBoard;

        private void Start()
        {
            _scorePulsate = GameObject.Find("CountDownTimerCanvas").GetComponent<Animator>();
        }

        private void HideObjects()
        {
            foreach (GameObject hideableObject in objectsToHide)
            {
                if (hideableObject == null) continue;
                hideableObject.SetActive(false);
            }
        }
        
        private void ShowObjects()
        {
            foreach (GameObject showableObject in objectsToShow) showableObject.SetActive(true);
        }

        public void EndGame(bool timerAtZero, float timerTime)
        {
            FMODUnity.RuntimeManager.PlayOneShot(!timerAtZero ? winSoundPath : loseSoundPath, transform.position);

            _scorePulsate.SetBool(GameOver, true);
            
            HideObjects();
            ShowObjects();
            
            _highscoreBoard = GameObject.Find("ScoreBoard").GetComponent<Scoreboard>();
            _entryData.entryName = "Player";
            _entryData.entryScore = (int) timerTime;
            _highscoreBoard.AddEntry(_entryData);
            
        }
    }
}
