using Enemy;
using UnityEngine;
using UnityEngine.AI;

namespace PillarOfLight
{
    public class GlueBomb : MonoBehaviour
    {
        public GameObject arrowTip;
//        public GameObject explosionEffect;
        public GameObject smokeEffect;
        
        public float blastRadius = 10f;

        private bool _glueBombArrowReady = false;
        private Collider[] _glueBombOverlapResults = new Collider[20];
//        private PowerUpManager _powerUpManager;
        
//        private void Start()
//        {
//            _powerUpManager = GameObject.Find("PowerUpsManagers").GetComponent<PowerUpManager>();
//        }

        private void Update()
        {
            if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger) && !_glueBombArrowReady)
            {
//                _powerUpManager.bombArrowAcquired = false;
                _glueBombArrowReady = true;
                smokeEffect.SetActive(true);
            }
        }

        public bool GlueBombArrowReady()
        {
            return _glueBombArrowReady;
        }

        public void Explode()
        {
//            var position = arrowTip.transform.position;
//            Instantiate(explosionEffect, position, arrowTip.transform.rotation);
            
            Physics.OverlapSphereNonAlloc(transform.position, blastRadius, _glueBombOverlapResults);

            foreach (Collider nearObject in _glueBombOverlapResults)
            {
                if (nearObject == null) continue;
                if (nearObject.transform.name != "Monster" && nearObject.transform.name != "LargeMonster") continue;
                nearObject.transform.parent.GetComponent<EnemyBehaviour>().GlueBomb();
            }
        }
    }
}
