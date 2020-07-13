using Crate;
using UnityEngine;

namespace TargetShooterMiniGame
{
    public class TargetHealth : MonoBehaviour, IDamageable
    {
        public GameObject explosionParticles;
        public StartGame startGame;

        public void Damage(int amount)
        {
            DestroySign();
        }

        private void DestroySign()
        {
            Instantiate(explosionParticles, transform.position, transform.rotation);
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
