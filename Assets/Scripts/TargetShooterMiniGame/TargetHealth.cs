using System;
using Crate;
using UnityEngine;

namespace TargetShooterMiniGame
{
    public class TargetHealth : MonoBehaviour
    {
        [SerializeField] private GameObject explosionParticles;
        
        private CountDownTimer _countDownTimer;

        private void Start()
        {
            _countDownTimer = GameObject.Find("CountDownTimerTextTMP").GetComponent<CountDownTimer>();
        }

        public void Damage()
        {
            Instantiate(explosionParticles, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            
            _countDownTimer.IncreaseTimer(3f);
            
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
