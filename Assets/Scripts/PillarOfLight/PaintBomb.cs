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

        private void Awake()
        {
            _specialAbilitiesBar = GameObject.Find("AbilitiesSlider").GetComponent<SpecialAbilitiesBar>();
        }

        private void Update()
        {
            if (OVRInput.GetDown(OVRInput.Button.Three) && !_bombArrowReady && _specialAbilitiesBar.IsPowerFull())
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

            Collider[] colliders = Physics.OverlapSphere(position, blastRadius);

            foreach (var nearObject in colliders)
            {
                if (nearObject.transform.name == "Monster")
                {
                    Destroy(nearObject.transform.parent.gameObject);
                }
            }
        }
    }
}
