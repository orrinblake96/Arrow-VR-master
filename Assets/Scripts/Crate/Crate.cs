using System;
using UnityEngine;

namespace _BowAndArrow.Scripts.Crate
{
    public class Crate : MonoBehaviour, IDamageable
    {
        public GameObject destroyedCrate;

        public void Damage(int amount)
        {
            DestroyCrate();
        }

        private void DestroyCrate()
        {
            Instantiate(destroyedCrate, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
