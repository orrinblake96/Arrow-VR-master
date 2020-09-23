using Crate;
using UnityEngine;
using WaveBasedLevel;

namespace PillarOfLight
{
    public class DestroyLargeEnemy : MonoBehaviour, IDamageable
    {
        public GameObject explosionParticles;
        
        private int _enemyHealth = 50;
        private WaveScore _waveScoreBoard;
        
        private void Awake()
        {
            _waveScoreBoard = GameObject.FindGameObjectWithTag("WaveScoreBoard").GetComponent<WaveScore>();
        }
        
        public void Damage(int amount)
        {
            _enemyHealth -= amount;

            if (_enemyHealth > 0) return;
            Instantiate(explosionParticles, transform.position + Vector3.up, transform.rotation);
            _waveScoreBoard.IncreaseCurrentScore(100, transform);
            Destroy(gameObject);
        }
    }
}
