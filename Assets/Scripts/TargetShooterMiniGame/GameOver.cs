using System;
using FMODUnity;
using UnityEngine;

namespace TargetShooterMiniGame
{
    public class GameOver : MonoBehaviour
    {
        public  TargetDestructionTimer timer;
        public GameObject exitSign;
        public GameObject replaySign;
        
        private bool _timerStopped;
        private StudioEventEmitter _gameOverSound;

        private void Start()
        {
            _gameOverSound = GetComponent<StudioEventEmitter>();
        }

        private void Update()
        {
            //When all targets are destroyed stop the timer
            if (gameObject.transform.childCount > 0 || _timerStopped) return;
            _gameOverSound.Play();
            _timerStopped = true;
            timer.StopTimer();
            exitSign.SetActive(true);
            replaySign.SetActive(true);
        }
    }
}
