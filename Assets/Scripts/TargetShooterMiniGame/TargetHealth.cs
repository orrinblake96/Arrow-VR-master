using Crate;
using UnityEngine;

namespace TargetShooterMiniGame
{
    public class TargetHealth : MonoBehaviour, IDamageable
    {
        public void Damage(int amount)
        {
            DestroyCrate();
        }

        private void DestroyCrate()
        {
            Destroy(gameObject);
        }
    }
}
