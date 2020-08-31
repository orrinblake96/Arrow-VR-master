using System;
using UnityEngine;
using UnityEngine.UI;

namespace PillarOfLight
{
    public class SpecialAbilitiesBar : MonoBehaviour
    {
        public Image sliderImage;
        public  ParticleSystem[] powerParticles;
        
        private Slider _powerValue;

        private void Awake()
        {
            _powerValue = gameObject.GetComponent<Slider>();
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                IncrementPower(.1f);
            }
            
            if (Input.GetKeyDown(KeyCode.R))
            {
                ResetPower();
            }
            
        }

        public void IncrementPower(float powerGained)
        {
            _powerValue.value += powerGained;

            if (_powerValue.value < 0.3f)
            {
                powerParticles[0].Emit(50);
                return;
            }

            if (_powerValue.value >= 0.3f && _powerValue.value <= 0.5f)
            {
                sliderImage.color = Color.yellow;
                powerParticles[1].Emit(50);
                return;
            }

            if (_powerValue.value > 0.5f)
            {
                sliderImage.color = Color.green;
                powerParticles[2].Emit(50);
            }
        }

        public void ResetPower()
        {
            _powerValue.value = 0f;
            sliderImage.color = Color.red;
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
