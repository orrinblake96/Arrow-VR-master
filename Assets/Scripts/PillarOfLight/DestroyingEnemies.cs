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
//        private LevelManager _levelManager; //Remove
        private int _enemyHealth = 10;
        
        private void Awake()
        {
//            _levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>(); //Remove
            
            if (GameObject.FindGameObjectWithTag("WaveScoreBoard") != null)
            {
                _waveScoreBoard = GameObject.FindGameObjectWithTag("WaveScoreBoard").GetComponent<WaveScore>();
                _scoreBoardExists = true;
            }
        }

//        private void OnDestroy()
//        {
//            if (_levelManager.IsSceneChanging()) return;
//            
//            Instantiate(explosionParticles, transform.position + Vector3.up, transform.rotation); //Remove
//            
//            if (_scoreBoardExists)
//            {
//                _waveScoreBoard.IncreaseCurrentScore(25);
//            }
//        }

        public void Damage(int amount)
        {
//            if (_levelManager.IsSceneChanging()) return; //Remove

            // Damage and check if dead
            _enemyHealth -= amount;
            if (_enemyHealth > 0) return;
            
            
            // Explode with effects, increase overall score and destroy enemy
            var enemyTransform = transform;
            Instantiate(explosionParticles, enemyTransform.position + Vector3.up, enemyTransform.rotation);
            if (_scoreBoardExists)
            {
                _waveScoreBoard.IncreaseCurrentScore(25);
            }
            Destroy(gameObject);
        }
    }
}
