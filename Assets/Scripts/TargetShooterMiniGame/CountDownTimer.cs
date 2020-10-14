using System;
using UnityEngine;
using TMPro;

namespace TargetShooterMiniGame
{
    public class CountDownTimer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _timerUI;
        [SerializeField] private float mainTimer;

        private float _timer;
        private bool _canCount = true;
        private bool _doOnce = false;
        private bool _gameOverOnce = false;
        private bool _timerStopped = false;
        private CountDownTimerGameOver _gameOver;

        private void Start()
        {
            _timer = mainTimer;
            _gameOver = GetComponent<CountDownTimerGameOver>();
        }

        private void Update()
        {
            if (_timerStopped)
            {
                if (!_gameOverOnce)
                {
                    _gameOver.EndGame();
                    _gameOverOnce = true;
                }
                return;
            }
            
            if(_timer >= 0.0f && _canCount)
            {
                _timer -= Time.deltaTime;
                _timerUI.text = _timer.ToString("00");
            }
            else if (_timer <= 0.0f && !_doOnce)
            {
                _canCount = false;
                _doOnce = true;
                _timerUI.text = "00";
                _timer = 0.0f;
                
                _gameOver.EndGame();
            }
        }

        public void IncreaseTimer(float timeIncreaseAmount)
        {
            if (_timerStopped) return;
            
            _timer += timeIncreaseAmount;
            _timerUI.text = _timer.ToString("00");
        }

        public bool TimerStopped
        {
            get => _timerStopped;
            set => _timerStopped = value;
        }
    }
}
