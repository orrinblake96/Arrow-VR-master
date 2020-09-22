using System;
using UnityEngine;

namespace PillarOfLight
{
    public class PaintBomb : MonoBehaviour
    {
        public GameObject arrowTip;
        public GameObject explosionEffect;
        public GameObject smokeEffect;
        
        public float blastRadius = 10f;

        private bool _bombArrowReady = false;
        private Collider[] _paintBombOverlapResults = new Collider[20];
        private PowerUpManager _powerUpManager;

        private void Start()
        {
            _powerUpManager = GameObject.Find("PowerUpsManagers").GetComponent<PowerUpManager>();
        }

        private void Update()
        {
            if (OVRInput.GetDown(OVRInput.Button.Three) && !_bombArrowReady && _powerUpManager.bombArrowAcquired)
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
            Instantiate(explosionEffect, position, arrowTip.transform.rotation);
            
            Physics.OverlapSphereNonAlloc(transform.position, 5f, _paintBombOverlapResults);

            foreach (Collider nearObject in _paintBombOverlapResults)
            {
                if (nearObject == null) continue;
                if (nearObject.transform.name != "Monster") continue;
                Destroy(nearObject.transform.parent.gameObject);
            }
        }
    }
}
