using System;
using FMODUnity;
using UnityEngine;

namespace PillarOfLight
{
    public class PaintBomb : MonoBehaviour
    {
        public GameObject arrowTip;
        public GameObject smokeEffect;
        
        public float blastRadius = 10f;

        [SerializeField] private LayerMask layersToInclude;

        [Header("Bomb Effects")] 
        [SerializeField] private StudioEventEmitter bombEffectSound;
        
        private bool _bombArrowReady = false;
        private Collider[] _paintBombOverlapResults = new Collider[20];
        private PowerUpManager _powerUpManager;
        
        private GameObject _explosionEffectGameObject;
        private ParticleSystem _explosionEffect;

        private void Start()
        {
            _powerUpManager = GameObject.Find("PowerUpsManagers").GetComponent<PowerUpManager>();
            _explosionEffectGameObject = GameObject.Find("BigExplosion");
            _explosionEffect = _explosionEffectGameObject.GetComponent<ParticleSystem>();
        }

        private void Update()
        {
            if (OVRInput.GetDown(OVRInput.Button.Three) && _powerUpManager.bombArrowAcquired && !_bombArrowReady)
            {
                _powerUpManager.bombArrowAcquired = false;
                _bombArrowReady = true;
                smokeEffect.SetActive(true);
            }
        }

        public bool BombArrowReady()
        {
            return _bombArrowReady;
        }

        public void Explode()
        {
            var position = arrowTip.transform.position;
            // Position & play bomb effects
            _explosionEffectGameObject.transform.position = position;
            _explosionEffect.Play();
            bombEffectSound.Play();
            
            Physics.OverlapSphereNonAlloc(transform.position, 5f, _paintBombOverlapResults, layersToInclude);
            
            foreach (Collider nearObject in _paintBombOverlapResults)
            {
                if (nearObject == null) continue;
                if (nearObject.transform.name == "LargeMonster")
                {
                    nearObject.transform.parent.GetComponent<DestroyLargeEnemy>().Damage(50);
                    continue;
                }
                nearObject.transform.parent.GetComponent<DestroyingEnemies>().Damage(10);
            }
        }
    }
}
