using System;
using Crate;
using UnityEngine;

namespace PillarOfLight
{
    public class ObtainPowerUp : MonoBehaviour, IDamageable
    {
        private PowerUpManager _powerUpManager;
        private SpawnRandomPowerUp _spawnRandomPowerUp;
        private void Start()
        {
            _powerUpManager = GameObject.Find("PowerUpsManagers").GetComponent<PowerUpManager>();
            _spawnRandomPowerUp = GameObject.Find("PowerUpSpawnPoints").GetComponent<SpawnRandomPowerUp>();
        }

        public void Damage(int amount)
        {
            if(gameObject.name == "slowTimePowerupBox(Clone)") _powerUpManager.slowTimeAcquired = true;
            if (gameObject.name == "paintBombPowerupBox(Clone)") _powerUpManager.bombArrowAcquired = true;
            
            // Only 2 boxes allowed at a time
            _spawnRandomPowerUp._powerUpsSpawnedCount--;
            
            Destroy(gameObject);
        }
    }
}
