using Crate;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PillarOfLight
{
    public class DestroyLargeEnemy : MonoBehaviour, IDamageable
    {
        [SerializeField] private GameObject explosionParticles;
        [SerializeField] private GameObject[] hitParticles;
        
        private int _enemyHealth = 50;
        private WaveScore _waveScoreBoard;
        
        private void Awake()
        {
            _waveScoreBoard = GameObject.FindGameObjectWithTag("WaveScoreBoard").GetComponent<WaveScore>();
        }

        //Debug code
//        private void Update()
//        {
//            if (Input.GetKeyDown(KeyCode.K))
//            {
//                Damage(10);
//            }
//        }

        public void Damage(int amount)
        {
            _enemyHealth -= amount;

            if (_enemyHealth > 0)
            {
                GameObject newHitParticles = Instantiate(hitParticles[Random.Range(0, hitParticles.Length)],
                    transform.position + (Vector3.up * 3), Quaternion.Euler(-90, 0, 0));
                Destroy(newHitParticles, 2f);
                return;
            }

            Instantiate(explosionParticles, transform.position + Vector3.up, transform.rotation);
            _waveScoreBoard.IncreaseCurrentScore(100, transform);
            Destroy(gameObject);
        }
    }
}
