using System.Collections;
using Crate;
using FMODUnity;
using UnityEngine;

namespace TargetShooterMiniGame
{
    public class StartTargetHealth : MonoBehaviour, IDamageable
    {
        public GameObject[] explosionParticles;
        public GameObject startUi;
        public GameObject arrowColourUi;
        public GameObject destroyableTargets;
        public CountDownTimer timer;
        
        private StudioEventEmitter _timerStartSound;
        private Material _targetMaterial;

        private void Start()
        {
            _timerStartSound = GetComponent<StudioEventEmitter>();
            _targetMaterial = GetComponent<MeshRenderer>().materials[0];
            Debug.Log("Material: " + _targetMaterial.name);
        }
        
        //Debug code
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Damage(10);
            }
        }

        public void Damage(int amount)
        {
            if (_targetMaterial.name.Contains("Red"))
            {
                StartCoroutine(DestroySign(explosionParticles[0]));
            } 
            else if (_targetMaterial.name.Contains("Green"))
            {
                StartCoroutine(DestroySign(explosionParticles[1]));
            }
            else if(_targetMaterial.name.Contains("Blue"))
            {
                StartCoroutine(DestroySign(explosionParticles[2]));
            }
        }

        private IEnumerator DestroySign(GameObject explosionParticle)
        {
            Instantiate(explosionParticle, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            
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
