using System;
using System.Collections;
using Crate;
using FMODUnity;
using UnityEngine;

namespace TargetShooterMiniGame
{
    public class TargetHealth : MonoBehaviour, IDamageable
    {
        public GameObject explosionParticles;
        public StartGame startGame;

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
            Instantiate(explosionParticles, transform.position, transform.rotation);

            if (gameObject.name.Equals("StartTarget"))
            {
                gameObject.GetComponent<MeshRenderer>().enabled = false;
                gameObject.GetComponent<BoxCollider>().enabled = false;
                _timerStartSound.Play();
                yield return new WaitForSeconds(3f);
            }

            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
