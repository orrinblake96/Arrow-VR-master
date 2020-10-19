using TMPro;
using UnityEngine;

namespace AccuracyMiniGame
{
    public class AccuracyScoreManager : MonoBehaviour
    {
        private int _currentScore;
        private TextMeshProUGUI _score;

        void Start()
        {
            _score = GetComponent<TextMeshProUGUI>();
        }

        public void UpdateScore(int amount)
        {
            _currentScore += amount;
            _score.text = _currentScore.ToString();
        }
    }
}
