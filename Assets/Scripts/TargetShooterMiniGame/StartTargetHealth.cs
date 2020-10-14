﻿using System.Collections;
using Crate;
using FMODUnity;
using UnityEngine;

namespace TargetShooterMiniGame
{
    public class StartTargetHealth : MonoBehaviour, IDamageable
    {
        public GameObject explosionParticles;
        public GameObject startUi;
        public GameObject arrowColourUi;
        public GameObject destroyableTargets;
        public CountDownTimer timer;
        
        private StudioEventEmitter _timerStartSound;

        private void Start()
        {
            _timerStartSound = GetComponent<StudioEventEmitter>();
        }

        public void Damage(int amount)
        {
            StartCoroutine(DestroySign());
        }

        private IEnumerator DestroySign()
        {
            Instantiate(explosionParticles, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider>().enabled = false;
            _timerStartSound.Play();
            startUi.SetActive(false);
            arrowColourUi.SetActive(true);
            
            yield return new WaitForSeconds(3f);
            
            destroyableTargets.SetActive(true);
            timer.enabled = true;
            
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}