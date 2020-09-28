using System.Collections;
using Audio;
using PillarOfLight;
using UnityEngine;

namespace Enemy
{
    public class WaveSpawner : MonoBehaviour
    {
        // wave system states
        private enum SpawnState { Spawning, Waiting, Counting }
        
        public Waves[] waves;
        public float timeBetweenWaves = 4f;
        public Transform[] spawnPoints;
        [HideInInspector] public float currentRoundNumber = 0f;
        public string soundPath;
        public GameObject largeEnemy;

        private int _nextWave = 0;
        private float _waveCountdown = 0f;
        private float _roundCount = 0;
        private float _nextWaveSpawnCountIncrease = 0f;
        private float _searchEnemyAliveCountdown = 1f;
        private SpawnState _state = SpawnState.Counting;
        private GameObject _pillarOfLight;
        private SpawnRandomPowerUp _spawnRandomPowerUp;

        private void Start()
        {
            // begin countdown for spawning
            _waveCountdown = timeBetweenWaves;
            
            _pillarOfLight = GameObject.Find("PillarOfLightTarget");

            _spawnRandomPowerUp = GameObject.Find("PowerUpSpawnPoints").GetComponent<SpawnRandomPowerUp>();
        }

        private void Update()
        {
            if (!_pillarOfLight.activeSelf) return;
            
            // if spawn system is waiting for enemies to be killed
            if (_state == SpawnState.Waiting)
            {
                // Check if any enemies alive
                if (!EnemyIsAlive())
                {
                   BeginNextWave(); 
                }
                else
                {
                    return;
                }
            }
            
            // if timer is finished and nothing is spawning then spawn next wave
            if (_waveCountdown <= 0)
            {
                if (_state != SpawnState.Spawning)
                {
                    // Spawn certain wave depending on level
                    StartCoroutine(SpawnWave(waves[_nextWave]));
                }
            }
            else
            {
                // if timers not finished then continue to countdown
                _waveCountdown -= Time.deltaTime;
            }
        }

        private void BeginNextWave()
        {
            // set spawn system state to counting down then increment wave index
            // Loop back to beginning if all waves completed
            _state = SpawnState.Counting;
            _waveCountdown = timeBetweenWaves;

            if (_nextWave + 1 > waves.Length - 1)
            {
                _nextWave = 0;
                
                // Increase number of enemies spawned, speed each agent moves & time waited before next wave 
                currentRoundNumber++;
                RoundCount++;
                _nextWaveSpawnCountIncrease += 1;
                if (timeBetweenWaves > 0) timeBetweenWaves -= 1;
            }
            else
            {
                _nextWave++;
            }
        }

        // Check if enemies are all dead every 1 second
        private bool EnemyIsAlive()
        {
            _searchEnemyAliveCountdown -= Time.deltaTime;
            if (_searchEnemyAliveCountdown <= 0f)
            {
                _searchEnemyAliveCountdown = 1f;
                
                if (GameObject.FindGameObjectWithTag("Enemy") == null)
                {
//                    _waveCount++;
                    _spawnRandomPowerUp.SpawnPowerUp();
                    FMODUnity.RuntimeManager.PlayOneShot(soundPath, transform.position);
                    _pillarOfLight.GetComponent<PillarHealth>().ResetPillarHits(0);
                    return false;
                }
            }
            return true;
        }

        private IEnumerator SpawnWave(Waves wave)
        {
            // set wave system to spawning state
            _state = SpawnState.Spawning;
            
            // Spawn enemies at defined spawn-rate intervals
            // Increase spawn rate with each round won by the player
            for (int i = 0; i < (wave.spawnCount + _nextWaveSpawnCountIncrease); i++)
            {
                SpawnEnemy(wave.enemyTransforms[Random.Range(0, wave.enemyTransforms.Length)]);
                yield return new WaitForSeconds(1f/(wave.spawnRate + currentRoundNumber));
            }
            
            if(Random.value > 0.7) SpawnEnemy(largeEnemy.transform);

            // After all enemies spawned set spawn system state to waiting
            _state = SpawnState.Waiting;
            
            yield break;
        }

        private void SpawnEnemy(Transform enemy)
        {
            // Spawn enemies at 1 of 3 pre-defined spawn points
            Transform sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(enemy, sp.position, sp.rotation);
        }
        
        // Calculating random power-up drops
        public float RoundCount
        {
            get => _roundCount;
            set => _roundCount = value;
        }
    }
}
