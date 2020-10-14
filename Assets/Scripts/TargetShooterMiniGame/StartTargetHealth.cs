using System.Collections;
using Crate;
using FMODUnity;
using UnityEngine;

namespace TargetShooterMiniGame
{
    public class StartTargetHealth : MonoBehaviour, IDamageable
    {
        public GameObject explosionParticles;
        public GameObject startUi;
        public GameObject destroyableTargets;
        public TargetDestructionTimer timer;
        
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
            
            yield return new WaitForSeconds(3f);
            
            startUi.SetActive(false);
            destroyableTargets.SetActive(true);
            timer.enabled = true;
            
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
