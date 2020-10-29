using Enemy;
using UnityEngine;

namespace PillarOfLight
{
    public class SpawnRandomPowerUp : MonoBehaviour
    {
        [Header("Spawn Attributes")]
        public Transform[] spawnPoints;
        public GameObject[] powerUps;
        [HideInInspector] public int _powerUpsSpawnedCount = 0;
        
        // Numbers to decide spawnability
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

        // ReSharper disable once CognitiveComplexity
        public void SpawnPowerUp()
        {
            if (!PlayerPrefs.HasKey("FirstPowerUpSpawned"))
            {

                // Increase counter to track number of power-ups in play
                _powerUpsSpawnedCount++;
                
                // Choose random power-up and position then spawn
                int powerupFirst = Random.Range(0, powerUps.Length);
                int spawnPointFirst = Random.Range(0, spawnPoints.Length);
                Instantiate(powerUps[powerupFirst], spawnPoints[spawnPointFirst].position, spawnPoints[spawnPointFirst].rotation);

                _waveSpawner.enabled = false;
                PlayerPrefs.SetInt("FirstPowerUpSpawned", 1);
                return;
            }

            // Check number of power-ups currently spawned (2 max)
            if (_powerUpsSpawnedCount >= 2) return;
            
            /*
             
            // Check Round and Health to determine spawn chance
            if ((int)_waveSpawner.RoundCount <= 1)
                _spawnabilityNumber = 0.3f;
            if ((int)_waveSpawner.RoundCount == 2)
                _spawnabilityNumber = 0.4f;
            if ((int)_waveSpawner.RoundCount == 3)
                _spawnabilityNumber = 0.5f;
            if ((int)_waveSpawner.RoundCount == 4)
                _spawnabilityNumber = 0.6f;
            if ((int)_waveSpawner.RoundCount == 5)
                _spawnabilityNumber = 0.7f;
            if ((int)_waveSpawner.RoundCount == 6)
                _spawnabilityNumber = 0.8f;
            if ((int)_waveSpawner.RoundCount >= 7)
                _spawnabilityNumber = 0.9f;
                
            
            
            // if random value is bigger then return and don't spawn a power-up
            if (Random.value > 0.3f) return;
            
            */
            
            
            // Increase counter to track number of power-ups in play
            _powerUpsSpawnedCount++;
            
            // Choose random power-up and position then spawn
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
