using System;
using Crate;
using UnityEngine;

namespace PillarOfLight
{
    public class ObtainPowerUp : MonoBehaviour, IDamageable
    {
        public GameObject fracturedBombPowerupBox;
        public GameObject fracturedTimePowerupBox;
        
        private PowerUpManager _powerUpManager;
        private SpawnRandomPowerUp _spawnRandomPowerUp;
        private void Start()
        {
            _powerUpManager = GameObject.Find("PowerUpsManagers").GetComponent<PowerUpManager>();
            _spawnRandomPowerUp = GameObject.Find("PowerUpSpawnPoints").GetComponent<SpawnRandomPowerUp>();
        }

        public void Damage(int amount)
        {
            if (gameObject.name == "slowTimePowerupBox(Clone)")
            {
                _powerUpManager.slowTimeAcquired = true;
                Instantiate(fracturedTimePowerupBox, transform.position, transform.rotation);
            }

            if (gameObject.name == "paintBombPowerupBox(Clone)")
            {
                _powerUpManager.bombArrowAcquired = true;
                Instantiate(fracturedBombPowerupBox, transform.position, transform.rotation);
            }
            
            // Only 2 boxes allowed at a time
            _spawnRandomPowerUp._powerUpsSpawnedCount--;
            
            Destroy(gameObject);
        }
    }
}
