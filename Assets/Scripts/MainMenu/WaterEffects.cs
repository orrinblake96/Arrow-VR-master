using UnityEngine;

namespace MainMenu
{
    public class WaterEffects : MonoBehaviour
    {
        [SerializeField] private GameObject waterSplashParticleEffect;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.name != "Tip") return;
            GameObject waterSplash = Instantiate(waterSplashParticleEffect, other.transform.position, Quaternion.Euler(-90, 0, 0));
            Destroy(waterSplash, 1f);
        }
    }
}
