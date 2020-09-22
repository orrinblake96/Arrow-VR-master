using System;
using Crate;
using UnityEngine;
using WaveBasedLevel;

namespace PillarOfLight
{
    public class DestroyingEnemies : MonoBehaviour
    {
        public GameObject explosionParticles;

        private WaveScore _waveScoreBoard;
        private bool _scoreBoardExists = false;
        private LevelManager _levelManager;
        
        private void Awake()
        {
            _levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
            
            if (GameObject.FindGameObjectWithTag("WaveScoreBoard") != null)
            {
                _waveScoreBoard = GameObject.FindGameObjectWithTag("WaveScoreBoard").GetComponent<WaveScore>();
                _scoreBoardExists = true;
            }
        }

        private void OnDestroy()
        {
            if (_levelManager.IsSceneChanging()) return;
            
            Instantiate(explosionParticles, transform.position + Vector3.up, transform.rotation);
            
            if (_scoreBoardExists)
            {
                _waveScoreBoard.IncreaseCurrentScore();
            }
        }
    }
}
