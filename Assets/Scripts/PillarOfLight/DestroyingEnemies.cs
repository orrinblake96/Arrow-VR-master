using System;
using Crate;
using UnityEngine;
using WaveBasedLevel;

namespace PillarOfLight
{
    public class DestroyingEnemies : MonoBehaviour, IDamageable
    {
        public GameObject explosionParticles;

        private WaveScore _waveScoreBoard;
        private bool _scoreBoardExists = false;
        private int _enemyHealth = 10;
        
        private void Awake()
        {
            if (GameObject.FindGameObjectWithTag("WaveScoreBoard") == null) return;
            _waveScoreBoard = GameObject.FindGameObjectWithTag("WaveScoreBoard").GetComponent<WaveScore>();
            _scoreBoardExists = true;
        }

        public void Damage(int amount)
        {

            // Damage and check if dead
            _enemyHealth -= amount;
            if (_enemyHealth > 0) return;

            // Explode with effects, increase overall score and destroy enemy
            var enemyTransform = transform;
            Instantiate(explosionParticles, enemyTransform.position + Vector3.up, Quaternion.Euler(-90f, 0f, 0f));
            if (_scoreBoardExists)
            {
                _waveScoreBoard.IncreaseCurrentScore(25, enemyTransform);
            }
            Destroy(gameObject);
        }
    }
}
