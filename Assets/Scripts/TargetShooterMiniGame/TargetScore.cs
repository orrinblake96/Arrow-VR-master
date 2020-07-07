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
        
        public void IncreaseCurrentScore()
        {
            _currentScore += 10;

            _scoreText.text = _currentScore.ToString();
        }
    }
}
