using System.Collections;
using TMPro;
using UnityEngine;

namespace PillarOfLight
{
    public class WaveScore : MonoBehaviour
    {
        public GameObject[] scoreMultiplierEffects;
        
        private int _currentScore;
        private int _currentMultiplier = 1;
        private TextMeshProUGUI _scoreText;
        private void Start()
        {
            _scoreText = GetComponent<TextMeshProUGUI>();
        }
        
        public void IncreaseCurrentScore(int pointsToAdd, Transform enemyPosition)
        {
            switch (_currentMultiplier)
            {
                case 1:
                    _currentScore += (pointsToAdd * _currentMultiplier);
                    _currentMultiplier++;
                    StopCoroutine(nameof(ScoreMultiplierTimer));
                    _scoreText.text = _currentScore.ToString();
                    Instantiate(scoreMultiplierEffects[0], enemyPosition.position + ( Vector3.up * 2 ), enemyPosition.rotation);
                    break;
                case 2:
                    _currentScore += (pointsToAdd * _currentMultiplier);
                    _currentMultiplier++;
                    StopCoroutine(nameof(ScoreMultiplierTimer));
                    Instantiate(scoreMultiplierEffects[1], enemyPosition.position + ( Vector3.up * 2 ), enemyPosition.rotation);
                    break;
                case 3:
                    _currentScore += (pointsToAdd * _currentMultiplier);
                    _currentMultiplier++;
                    StopCoroutine(nameof(ScoreMultiplierTimer));
                    Instantiate(scoreMultiplierEffects[2], enemyPosition.position + ( Vector3.up * 2 ), enemyPosition.rotation);
                    break;
                case 4:
                    _currentScore += (pointsToAdd * _currentMultiplier);
                    _currentMultiplier++;
                    StopCoroutine(nameof(ScoreMultiplierTimer));
                    Instantiate(scoreMultiplierEffects[3], enemyPosition.position + ( Vector3.up * 2 ), enemyPosition.rotation);
                    break;
                case 5:
                    _currentScore += (pointsToAdd * _currentMultiplier);
                    _currentMultiplier++;
                    StopCoroutine(nameof(ScoreMultiplierTimer));
                    Instantiate(scoreMultiplierEffects[4], enemyPosition.position + ( Vector3.up * 2 ), enemyPosition.rotation);
                    break;
                case 6:
                    _currentScore += (pointsToAdd * _currentMultiplier);
                    StopCoroutine(nameof(ScoreMultiplierTimer));
                    Instantiate(scoreMultiplierEffects[5], enemyPosition.position + ( Vector3.up * 2 ), enemyPosition.rotation);
                    break;
            } 
            StartCoroutine(nameof(ScoreMultiplierTimer));
            _scoreText.text = _currentScore.ToString();
        }

        IEnumerator ScoreMultiplierTimer()
        {
            yield return new WaitForSeconds(5f);
            _currentMultiplier = 1;
        }
        
        // Calculating random power-up drops
        public int CurrentScore => _currentScore;
        
        // Get current score multiplier
        public int CurrentMultiplier => _currentMultiplier;
    }
}
