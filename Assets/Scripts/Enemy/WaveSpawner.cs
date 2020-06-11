﻿using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class WaveSpawner : MonoBehaviour
    {
        // wave system states
        private enum SpawnState { Spawning, Waiting, Counting }
        
        public Waves[] waves;
        public float timeBetweenWaves = 5f;
        public Transform[] spawnPoints;

        private int _nextWave = 0;
        private float _waveCountdown = 0f;
        private float _searchEnemyAliveCountdown = 1f;
        private SpawnState _state = SpawnState.Counting;

        private void Start()
        {
            // begin countdown for spawning
            _waveCountdown = timeBetweenWaves;
        }

        private void Update()
        {
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
            }
            else
            {
                _nextWave++;
            }
        }

        // Check if enemies are all dead every 1 second
        bool EnemyIsAlive()
        {
            _searchEnemyAliveCountdown -= Time.deltaTime;
            if (_searchEnemyAliveCountdown <= 0f)
            {
                _searchEnemyAliveCountdown = 1f;
                
                //*********************** can be optimised ***********************
                if (GameObject.FindGameObjectWithTag("Enemy") == null)
                {
                    return false;
                }
            }
            return true;
        }

        IEnumerator SpawnWave(Waves wave)
        {
            // set wave system to spawning state
            _state = SpawnState.Spawning;
            
            // Spawn enemies at defined spawn-rate intervals
            for (int i = 0; i < wave.spawnCount; i++)
            {
                SpawnEnemy(wave.enemyTransform);
                yield return new WaitForSeconds(1f/wave.spawnRate);
            }

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
    }
}