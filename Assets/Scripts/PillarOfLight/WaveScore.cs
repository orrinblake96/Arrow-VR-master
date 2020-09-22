using TMPro;
using UnityEngine;

namespace WaveBasedLevel
{
    public class WaveScore : MonoBehaviour
    {
        private int _currentScore;
        private TextMeshProUGUI _scoreText;
        private void Start()
        {
            _scoreText = GetComponent<TextMeshProUGUI>();
        }
        
        public void IncreaseCurrentScore()
        {
            _currentScore += 25;

            _scoreText.text = _currentScore.ToString();
        }
        
        // Calculating random power-up drops
        public int CurrentScore => _currentScore;
    }
}
