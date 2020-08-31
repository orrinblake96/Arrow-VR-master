using System;
using UnityEngine;

namespace PillarOfLight
{
    public class PaintBomb : MonoBehaviour
    {
        public GameObject arrowTip;
        public GameObject explosionEffect;
        public float blastRadius = 5f;

        private ArrowTipColorChecker _arrowTipColorChecker;

        private void Awake()
        {
            _arrowTipColorChecker = GameObject.Find("Tip").GetComponent<ArrowTipColorChecker>();
        }

        public void Explode()
        {
            var position = arrowTip.transform.position;
            Instantiate(explosionEffect, position, arrowTip.transform.rotation);

            Collider[] colliders = Physics.OverlapSphere(position, blastRadius);

            foreach (var nearObjects in colliders)
            {
                
                if (nearObjects.transform.parent.gameObject.CompareTag("Enemy"))
                {
                    Destroy(nearObjects.transform.parent.gameObject);
//                    _arrowTipColorChecker.DestroyDuringExplosion(nearObjects);
                }
            }
        }
    }
}
