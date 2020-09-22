using Enemy;
using UnityEngine;
using WaveBasedLevel;
//using Random = System.Random;

namespace PillarOfLight
{
    public class SpawnRandomPowerUp : MonoBehaviour
    {
        public Transform[] spawnPoints;
        public GameObject[] powerUps;
        [HideInInspector] public int _powerUpsSpawnedCount = 0;

        private PillarHealth _pillarHealth;
        private WaveScore _waveScore;
        private WaveSpawner _waveSpawner;
        private int[] _calculationValues = new int[3];
        private float _spawnabilityNumber;

        private void Start()
        {
            _pillarHealth = GameObject.Find("PillarOfLightTarget").GetComponent<PillarHealth>();
            _waveScore = GameObject.Find("ScorenumberTMP").GetComponent<WaveScore>();
            _waveSpawner = GameObject.Find("GM").GetComponent<WaveSpawner>();
        }

//        private void Update()
//        {
//            
//            if ( (Input.GetKeyDown(KeyCode.Space) || OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger)) && _powerUpsSpawnedCount < 1 )
//            {
//                _powerUpsSpawnedCount++;
//                Random random = new Random();
//                int powerup = random.Next(0, powerUps.Length);
//                
//                random = new Random();
//                int spawnPoint = random.Next(0, spawnPoints.Length);
//
//                Instantiate(powerUps[powerup], spawnPoints[spawnPoint].position, spawnPoints[spawnPoint].rotation);
//            }
//        }

        public void SpawnPowerUp()
        {
            // Check number of power-ups currently spawned (2 max)
            if (_powerUpsSpawnedCount >= 2) return;
            
            // Check Round and Health to determine spawn chance
            if ((int)_waveSpawner.RoundCount <= 1 || _pillarHealth.CurrentHealth == 100)
                _spawnabilityNumber = 0.3f;
            if ((int)_waveSpawner.RoundCount == 2 || _pillarHealth.CurrentHealth == 80)
                _spawnabilityNumber = 0.4f;
            if ((int)_waveSpawner.RoundCount == 3 || _pillarHealth.CurrentHealth == 70)
                _spawnabilityNumber = 0.5f;
            if ((int)_waveSpawner.RoundCount == 4 || _pillarHealth.CurrentHealth == 50)
                _spawnabilityNumber = 0.6f;
            if ((int)_waveSpawner.RoundCount == 5 || _pillarHealth.CurrentHealth == 40)
                _spawnabilityNumber = 0.7f;
            if ((int)_waveSpawner.RoundCount == 6 || _pillarHealth.CurrentHealth == 20)
                _spawnabilityNumber = 0.8f;
            if ((int)_waveSpawner.RoundCount >= 7 || _pillarHealth.CurrentHealth == 10)
                _spawnabilityNumber = 0.9f;
            
            print("spawn Chance" + _spawnabilityNumber);
            
            if (Random.value > _spawnabilityNumber) return;

            _powerUpsSpawnedCount++;
            int powerup = Random.Range(0, powerUps.Length);
            
            int spawnPoint = Random.Range(0, spawnPoints.Length);

            Instantiate(powerUps[powerup], spawnPoints[spawnPoint].position, spawnPoints[spawnPoint].rotation);
            }

        private int[] GetCurrentValuesForCalculation()
        {
            Debug.Log("Health: " + _pillarHealth.CurrentHealth);
            Debug.Log("Score: " +_waveScore.CurrentScore);
            Debug.Log("Wave #: " +_waveSpawner.RoundCount);
            
//            _powerUpSpawned = true;
            _calculationValues[0] = _pillarHealth.CurrentHealth;
            _calculationValues[1] = _waveScore.CurrentScore;
            _calculationValues[2] = (int)_waveSpawner.RoundCount;
            
            return _calculationValues;
        }
    }
}
