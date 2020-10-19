using TMPro;
using UnityEngine;

namespace TargetShooterMiniGame
{
    public class TargetScore : MonoBehaviour
    {
        private int _currentScore;
        private TextMeshProUGUI _scoreText;
        private void Start()
        {
            _scoreText = GetComponent<TextMeshProUGUI>();
        }

        public void IncreaseCurrentScore(int amount = 10)
        {
            _currentScore += amount;

            _scoreText.text = _currentScore.ToString();
        }
    }
}
