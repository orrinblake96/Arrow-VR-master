using System;
using Crate;
using Greyman;
using UnityEngine;

namespace PillarOfLight
{
    public class DestroyingEnemies : MonoBehaviour, IDamageable
    {
        public GameObject explosionParticles;
        
        private  OffScreenIndicator _offScreenIndicator;
        private WaveScore _waveScoreBoard;
        private bool _scoreBoardExists;
        private int _enemyHealth = 10;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Damage(10);
            }
        }

        private void Awake()
        {
            if (GameObject.FindGameObjectWithTag("WaveScoreBoard") == null) return;
            _waveScoreBoard = GameObject.FindGameObjectWithTag("WaveScoreBoard").GetComponent<WaveScore>();
            _scoreBoardExists = true;
            
            //Off screen indicators;
            _offScreenIndicator = GameObject.Find("Logic").GetComponent<OffScreenIndicator>();
            if (gameObject.name.Contains("Red"))
            {
                _offScreenIndicator.AddIndicator(gameObject.transform, 0);
                return;
            } 
            if (gameObject.name.Contains("Green"))
            {
                _offScreenIndicator.AddIndicator(gameObject.transform, 1);
                return;
            } 
            if (gameObject.name.Contains("Blue"))
            {
                _offScreenIndicator.AddIndicator(gameObject.transform, 2);
            } 
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
                _offScreenIndicator.RemoveIndicator(gameObject.transform);
            }
            Destroy(gameObject);
        }
    }
}
