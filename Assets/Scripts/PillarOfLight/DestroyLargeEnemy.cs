using Crate;
using UnityEngine;

namespace PillarOfLight
{
    public class DestroyLargeEnemy : MonoBehaviour, IDamageable
    {
        public GameObject explosionParticles;
        
        private int _enemyHealth = 50;

        public void Damage(int amount)
        {
            _enemyHealth -= amount;
            
            print("Health: " + _enemyHealth);
            
            if (_enemyHealth <= 0)
            {
                Instantiate(explosionParticles, transform.position + Vector3.up, transform.rotation);
                Destroy(gameObject);
            }
        }
    }
}
