using System;
using UnityEngine;
using UnityEngine.UI;

namespace PillarOfLight
{
    public class SpecialAbilitiesBar : MonoBehaviour
    {
        private Slider _powerValue;
        private ParticleSystem _powerParticles;

        private void Awake()
        {
            _powerValue = gameObject.GetComponent<Slider>();
            _powerParticles = GameObject.Find("Power filling particles").GetComponent<ParticleSystem>();
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                IncrementPower(.1f);
            }
            
        }

        public void IncrementPower(float powerGained)
        {
            _powerValue.value += powerGained;
            
             _powerParticles.Emit(50);
        }

        public void ResetPower()
        {
            _powerValue.value = 0f;
        }

        public bool IsPowerCharged()
        {
            return _powerValue.value > 0f;
        }

        public float CurrentPower()
        {
            return _powerValue.value * 10f;
        }
    }
}
