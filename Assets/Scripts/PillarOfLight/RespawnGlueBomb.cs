using System.Collections;
using UnityEngine;

namespace PillarOfLight
{
    public class RespawnGlueBomb : MonoBehaviour
    {
        public void RespawnGlue(GameObject glueBomb)
        {
            StartCoroutine(Respawn(glueBomb));
        }

        private IEnumerator Respawn(GameObject glueBomb)
        {
            yield return new WaitForSeconds(Random.Range(30f, 50f));
            glueBomb.SetActive(true);
        }
    }
}
