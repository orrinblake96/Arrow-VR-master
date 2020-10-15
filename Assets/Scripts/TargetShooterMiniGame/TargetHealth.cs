using System;
using Crate;
using UnityEngine;

namespace TargetShooterMiniGame
{
    public class TargetHealth : MonoBehaviour
    {
        [SerializeField] private GameObject [] explosionParticles;
        
        private CountDownTimer _countDownTimer;
        private Material _targetMaterial;


        private void Start()
        {
            _countDownTimer = GameObject.Find("CountDownTimerTextTMP").GetComponent<CountDownTimer>();
        }
        
        //Debug code
//        private void Update()
//        {
//            if (Input.GetKeyDown(KeyCode.Space))
//            {
//                Damage();
//            }
//        }

        public void Damage()
        {
            _targetMaterial = GetComponent<MeshRenderer>().materials[0];
            
            if (_targetMaterial.name.Contains("Red"))
            {
                Instantiate(explosionParticles[0], transform.position, Quaternion.Euler(-90f, 0f, 0f));
            } 
            else if (_targetMaterial.name.Contains("Green"))
            {
                Instantiate(explosionParticles[1], transform.position, Quaternion.Euler(-90f, 0f, 0f));
            }
            else if(_targetMaterial.name.Contains("Blue"))
            {
                Instantiate(explosionParticles[2], transform.position, Quaternion.Euler(-90f, 0f, 0f));
            }

            _countDownTimer.IncreaseTimer(3f);
            
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
