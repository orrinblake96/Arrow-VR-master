using UnityEngine;

namespace PillarOfLight
{
    public class PaintBomb : MonoBehaviour
    {
        public GameObject arrowTip;
        public GameObject explosionEffect;

        public void Explode()
        {
            Instantiate(explosionEffect, arrowTip.transform.position, arrowTip.transform.rotation);
        }
    }
}
