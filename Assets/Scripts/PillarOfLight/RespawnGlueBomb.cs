using System.Collections;
using UnityEngine;

namespace PillarOfLight
{
    public class RespawnGlueBomb : MonoBehaviour
    {
        [SerializeField] private float waitTime = 5f;

        public void RespawnGlue(GameObject glueBomb)
        {
            StartCoroutine(Respawn(glueBomb));
        }

        private IEnumerator Respawn(GameObject glueBomb)
        {
            yield return new WaitForSeconds(waitTime);
            glueBomb.SetActive(true);
        }
    }
}
