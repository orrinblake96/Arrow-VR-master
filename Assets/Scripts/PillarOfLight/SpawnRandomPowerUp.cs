using System;
using Enemy;
using UnityEngine;
using WaveBasedLevel;
using Random = System.Random;

namespace PillarOfLight
{
    public class SpawnRandomPowerUp : MonoBehaviour
    {
        public Transform[] spawnPoints;
        public GameObject[] powerUps;

        private PillarHealth _pillarHealth;
        private WaveScore _waveScore;
        private WaveSpawner _waveSpawner;
        public int _powerUpsSpawnedCount = 0;

        private void Start()
        {
            _pillarHealth = GameObject.Find("PillarOfLightTarget").GetComponent<PillarHealth>();
            _waveScore = GameObject.Find("ScorenumberTMP").GetComponent<WaveScore>();
            _waveSpawner = GameObject.Find("GM").GetComponent<WaveSpawner>();
        }

        private void Update()
        {
            
            if ( (Input.GetKeyDown(KeyCode.Space) || OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger)) && _powerUpsSpawnedCount < 2 )
            {
                _powerUpsSpawnedCount++;
                Random random = new Random();
                int powerup = random.Next(0, powerUps.Length);
                
                random = new Random();
                int spawnPoint = random.Next(0, spawnPoints.Length);

                Instantiate(powerUps[powerup], spawnPoints[spawnPoint].position, spawnPoints[spawnPoint].rotation);
            }
        }

        private int GetCurrentValuesForCalculation()
        {
            Debug.Log("Health: " + _pillarHealth.CurrentHealth);
            Debug.Log("Score: " +_waveScore.CurrentScore);
            Debug.Log("Wave #: " +_waveSpawner.WaveCount);
            
//            _powerUpSpawned = true;
            
            return _waveScore.CurrentScore;
        }
    }
}
