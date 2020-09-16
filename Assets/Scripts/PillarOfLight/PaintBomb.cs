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
        private SpecialAbilitiesBar _specialAbilitiesBar;
        private Collider[] _paintBombOverlapResults = new Collider[20];

        private void Awake()
        {
            _specialAbilitiesBar = GameObject.Find("AbilitiesSlider").GetComponent<SpecialAbilitiesBar>();
        }

        private void Update()
        {
            if (OVRInput.GetDown(OVRInput.Button.Three) && !_bombArrowReady && _specialAbilitiesBar.IsPowerFull() )
            {
                _specialAbilitiesBar.ResetPower();
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
