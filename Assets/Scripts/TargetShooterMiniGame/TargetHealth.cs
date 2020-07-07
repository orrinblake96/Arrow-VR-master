using System;
using Crate;
using UnityEngine;

namespace TargetShooterMiniGame
{
    public class TargetHealth : MonoBehaviour, IDamageable
    {
        private TargetScore _targetScore;

        private void Start()
        {
            _targetScore = GameObject.Find("ScorenumberTMP").GetComponent<TargetScore>();
        }

        public void Damage(int amount)
        {
            DestroyCrate();
        }

        private void DestroyCrate()
        {
            _targetScore.IncreaseCurrentScore();
            print("Score");
            Destroy(gameObject);
        }
    }
}
