using UnityEngine;

namespace PillarOfLight
{
    public class PillarHealth : MonoBehaviour
    {
        public float startingHealth = 100;
        public float currentHealth;
        
        void Start()
        {
            currentHealth = startingHealth;
        }

        public void DamageTaken()
        {
            currentHealth -= 10;

            if(currentHealth <= 0) Destroy(gameObject);
        }
    }
}
