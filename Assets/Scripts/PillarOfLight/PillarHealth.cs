using UnityEngine;

namespace PillarOfLight
{
    public class PillarHealth : MonoBehaviour
    {
        public enum PillarState
        {
            TakingDamage,
            Attacking,
            Destroyed
        }
        public float startingHealth = 100;
        public float currentHealth;
        public Material pillarOfLightMaterial;

        private float _pillarColor = 255f;
        private Material _pillarOfLightMaterialInstance;
        
        private void Start()
        {
            // Make an instance of the material so its color changes back to white on Quitting
            _pillarOfLightMaterialInstance = Instantiate(pillarOfLightMaterial);
            GetComponent<MeshRenderer>().material = _pillarOfLightMaterialInstance;
            
            currentHealth = startingHealth;
        }

        public void DamageTaken()
        {
            currentHealth -= 10;
            
            // Changes pillar color from white to red the more times it takes damage
            _pillarColor -= 25.5f;
            _pillarOfLightMaterialInstance.color = new Color(255f/255f, _pillarColor/255f, _pillarColor/255f, 1f);

            if(currentHealth <= 0) Destroy(gameObject);
        }
    }
}
