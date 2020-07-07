using Crate;
using UnityEngine;

namespace TargetShooterMiniGame
{
    public class TargetHealth : MonoBehaviour, IDamageable
    {
        public GameObject explosionParticles;

        public void Damage(int amount)
        {
            DestroyCrate();
        }

        private void DestroyCrate()
        {
            Instantiate(explosionParticles, transform.position, transform.rotation);
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
