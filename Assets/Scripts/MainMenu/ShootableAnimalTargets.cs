using System;
using Crate;
using FMODUnity;
using PillarOfLight;
using UnityEngine;

namespace MainMenu
{
    public class ShootableAnimalTargets : MonoBehaviour, IDamageable
    {
        [SerializeField] private GameObject animalTargetToHide;
        [SerializeField] private RespawnGlueBomb respawnAnimalTarget;
        [SerializeField] private ParticleSystem animalHitParticleEffect;

        private StudioEventEmitter _eventEmitter;

        private void Awake()
        {
            _eventEmitter = GetComponent<StudioEventEmitter>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                Damage(10);
            }
        }

        public void Damage(int amount)
        {
            animalHitParticleEffect.Play();
            _eventEmitter.Play();
            animalTargetToHide.SetActive(false);
            
            respawnAnimalTarget.RespawnGlue(animalTargetToHide);
        }
    }
}
