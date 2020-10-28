using System;
using Crate;
using FMODUnity;
using TargetShooterMiniGame;
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

        [Header("Score")] 
        [SerializeField] private TargetScore _targetScore;
        
        private AccuracyGameManager _accuracyGameManager;
        private StudioEventEmitter _hitSound;

        private void Start()
        {
            _accuracyGameManager = GameObject.Find("GM").GetComponent<AccuracyGameManager>();
            _hitSound = GetComponentInParent<StudioEventEmitter>();
        }

//        private void Update()
//        {
//            if(Input.GetKeyDown(KeyCode.Space)) Damage(10);
//        }

        public void Damage(int amount)
        {
            _hitSound.Play();
            Instantiate(targetSmokeParticles, transform.position, transform.rotation);
            Instantiate(scoreNumberEffect, transform.position, transform.rotation);
            
            _targetScore.IncreaseCurrentScore(hitAmount);
            
            _accuracyGameManager.DisplayNextTarget(transform.parent.gameObject);

            transform.parent.gameObject.SetActive(false);
        }
    }
}
