﻿using UnityEngine;

namespace PillarOfLight
{
    public class PillarHealth : MonoBehaviour
    {
        
        public float startingHealth = 100;
        public Material pillarOfLightMaterial;
        public GameObject exitSign;
        public GameObject replaySign;
        public GameObject pillarExplosionEffect;
        
        private float _currentHealth;
        private float _pillarColor = 255f;
        private int _hitCount = 0;
        private Material _pillarOfLightMaterialInstance;
        private Collider[] _pillarExplosionOverlapResults = new Collider[20];

        private void Start()
        {
            // Make an instance of the material so its color changes back to white on Quitting
            _pillarOfLightMaterialInstance = Instantiate(pillarOfLightMaterial);
            GetComponent<MeshRenderer>().material = _pillarOfLightMaterialInstance;
            
            _currentHealth = startingHealth;
        }

        public void DamageTaken()
        {
            _currentHealth -= 10;

            // Changes pillar color from white to red the more times it takes damage
            _pillarColor -= 25.5f;
            _pillarOfLightMaterialInstance.color = new Color(255f/255f, _pillarColor/255f, _pillarColor/255f, 1f);
            
            // Check for damage taken then explode to destroy enemies
            _hitCount++;
            if (_hitCount == 2)
            {
                _hitCount = 0;

                GameObject explosionEffect = Instantiate(pillarExplosionEffect, transform.position + Vector3.down, transform.rotation);
                Destroy(explosionEffect, 4f);
                DestroyEnemies();
            }

            if (!(_currentHealth <= 0)) return;
            if(exitSign) exitSign.SetActive(true);
            if(replaySign) replaySign.SetActive(true);
            gameObject.SetActive(false);
        }

        private void DestroyEnemies()
        {
            Physics.OverlapSphereNonAlloc(transform.position, 5f, _pillarExplosionOverlapResults);

            foreach (Collider nearObject in _pillarExplosionOverlapResults)
            {
                if (nearObject == null) continue;
                if (nearObject.transform.name != "Monster") continue;
                Destroy(nearObject.transform.parent.gameObject);
            }
        }
        
        public void ResetPillarHits(int hitCount)
        {
            _hitCount = hitCount;
        }
        
        // Calculating random power-up drops
        public float CurrentHealth => _currentHealth;
    }
}
