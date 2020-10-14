using Crate;
using UnityEngine;

namespace TargetShooterMiniGame
{
    public class TargetHealth : MonoBehaviour
    {
        public GameObject explosionParticles;

        public void Damage()
        {
            Instantiate(explosionParticles, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
