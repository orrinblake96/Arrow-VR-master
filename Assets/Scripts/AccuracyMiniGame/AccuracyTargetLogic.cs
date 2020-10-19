using System;
using Crate;
using UnityEngine;

namespace AccuracyMiniGame
{
    public class AccuracyTargetLogic : MonoBehaviour, IDamageable
    {
        [Header("Hit Info")]
        [SerializeField] private int hitAmount;

        [Header("Target Effects")] 
        [SerializeField] private GameObject scoreNumberEffect;
        [SerializeField] private GameObject targetSmokeParticles;
        
        private AccuracyGameManager _accuracyGameManager;
        
        private void Start()
        {
            _accuracyGameManager = GameObject.Find("GM").GetComponent<AccuracyGameManager>();
        }

//        private void Update()
//        {
//            if(Input.GetKeyDown(KeyCode.Space)) Damage(10);
//        }

        public void Damage(int amount)
        {
            Instantiate(targetSmokeParticles, transform.position, transform.rotation);
            Instantiate(scoreNumberEffect, transform.position, transform.rotation);
            _accuracyGameManager.DisplayNextTarget(transform.parent.gameObject);
            
            transform.parent.gameObject.SetActive(false);
        }
    }
}
