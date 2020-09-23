﻿using System.Collections;
using TMPro;
using UnityEngine;

namespace WaveBasedLevel
{
    public class WaveScore : MonoBehaviour
    {
        private int _currentScore;
        private int _currentMultiplier = 1;
        private TextMeshProUGUI _scoreText;
        private void Start()
        {
            _scoreText = GetComponent<TextMeshProUGUI>();
        }
        
        public void IncreaseCurrentScore(int pointsToAdd)
        {
            switch (_currentMultiplier)
            {
                case 1:
                    _currentScore += (pointsToAdd * _currentMultiplier);
                    _currentMultiplier++;
                    StartCoroutine(ScoreMultiplierTimer());
                    _scoreText.text = _currentScore.ToString();
                    Debug.Log("Multiplier: " + _currentMultiplier);
                    return;
                case 2:
                    _currentScore += (pointsToAdd * _currentMultiplier);
                    _currentMultiplier++;
                    StopCoroutine(ScoreMultiplierTimer());
                    break;
                case 3:
                    _currentScore += (pointsToAdd * _currentMultiplier);
                    _currentMultiplier++;
                    StopCoroutine(ScoreMultiplierTimer());
                    break;
                case 4:
                    _currentScore += (pointsToAdd * _currentMultiplier);
                    _currentMultiplier++;
                    StopCoroutine(ScoreMultiplierTimer());
                    break;
            } 
            
            Debug.Log("Multiplier: " + _currentMultiplier);
            StartCoroutine(ScoreMultiplierTimer());
            _scoreText.text = _currentScore.ToString();
        }

        IEnumerator ScoreMultiplierTimer()
        {
            Debug.Log("Co Started ");
            yield return new WaitForSeconds(5f);
            _currentMultiplier = 1;
            Debug.Log("Co ended ");
        }
        
        // Calculating random power-up drops
        public int CurrentScore => _currentScore;
        
        // Get current score multiplier
        public int CurrentMultiplier => _currentMultiplier;
    }
}
