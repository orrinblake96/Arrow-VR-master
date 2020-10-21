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
        private PillarHealth _pillarHealth;
        private void Start()
        {
            _scoreText = GetComponent<TextMeshProUGUI>();
            _pillarHealth = GameObject.Find("PillarOfLightTarget").GetComponent<PillarHealth>();
        }
        
        public void IncreaseCurrentScore(int pointsToAdd, Transform enemyPosition)
        {
            if (_pillarHealth.CurrentHealth <= 0) return;
            
            switch (_currentMultiplier)
            {
                case 1:
                    _currentScore += (pointsToAdd * _currentMultiplier);
                    _currentMultiplier++;
                    StopCoroutine(nameof(ScoreMultiplierTimer));
                    _scoreText.text = _currentScore.ToString();
//                    Instantiate(scoreMultiplierEffects[0], enemyPosition.position + ( Vector3.up * 2 ), enemyPosition.rotation);
                    MoveMultiplierUi(scoreMultiplierEffects[0], enemyPosition);
                    break;
                case 2:
                    _currentScore += (pointsToAdd * _currentMultiplier);
                    _currentMultiplier++;
                    StopCoroutine(nameof(ScoreMultiplierTimer));
//                    Instantiate(scoreMultiplierEffects[1], enemyPosition.position + ( Vector3.up * 2 ), enemyPosition.rotation);
                    MoveMultiplierUi(scoreMultiplierEffects[1], enemyPosition);
                    break;
                case 3:
                    _currentScore += (pointsToAdd * _currentMultiplier);
                    _currentMultiplier++;
                    StopCoroutine(nameof(ScoreMultiplierTimer));
//                    Instantiate(scoreMultiplierEffects[2], enemyPosition.position + ( Vector3.up * 2 ), enemyPosition.rotation);
                    MoveMultiplierUi(scoreMultiplierEffects[2], enemyPosition);
                    break;
                case 4:
                    _currentScore += (pointsToAdd * _currentMultiplier);
                    _currentMultiplier++;
                    StopCoroutine(nameof(ScoreMultiplierTimer));
//                    Instantiate(scoreMultiplierEffects[3], enemyPosition.position + ( Vector3.up * 2 ), enemyPosition.rotation);
                    MoveMultiplierUi(scoreMultiplierEffects[3], enemyPosition);
                    break;
                case 5:
                    _currentScore += (pointsToAdd * _currentMultiplier);
                    _currentMultiplier++;
                    StopCoroutine(nameof(ScoreMultiplierTimer));
//                    Instantiate(scoreMultiplierEffects[4], enemyPosition.position + ( Vector3.up * 2 ), enemyPosition.rotation);
                    MoveMultiplierUi(scoreMultiplierEffects[4], enemyPosition);
                    break;
                case 6:
                    _currentScore += (pointsToAdd * _currentMultiplier);
                    StopCoroutine(nameof(ScoreMultiplierTimer));
//                    Instantiate(scoreMultiplierEffects[5], enemyPosition.position + ( Vector3.up * 2 ), enemyPosition.rotation);
                    MoveMultiplierUi(scoreMultiplierEffects[5], enemyPosition);
                    break;
            } 
            StartCoroutine(nameof(ScoreMultiplierTimer));
            _scoreText.text = _currentScore.ToString();
        }

        private void MoveMultiplierUi(GameObject multiplier, Transform enemyPos)
        {
            multiplier.transform.position = enemyPos.position + (Vector3.up);
//            multiplier.transform.rotation = enemyPos.rotation;
//            multiplier.SetActive(true);
            multiplier.GetComponent<ParticleSystem>().Play();
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
