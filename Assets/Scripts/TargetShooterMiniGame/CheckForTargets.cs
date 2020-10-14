using System;
using UnityEngine;

namespace TargetShooterMiniGame
{
    public class CheckForTargets : MonoBehaviour
    {
        private bool _stopTimer = false;
        private CountDownTimer _countDownTimer;

        private void Start()
        {
            _countDownTimer = GameObject.Find("CountDownTimerTextTMP").GetComponent<CountDownTimer>();
        }

        void Update()
        {
            //When all targets are destroyed stop the timer
            if (gameObject.transform.childCount > 0 || _stopTimer) return;

            _stopTimer = true;
            _countDownTimer.TimerStopped = true;
        }
    }
}
