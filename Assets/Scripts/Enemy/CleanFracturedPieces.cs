using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class CleanFracturedPieces : MonoBehaviour
    {

        private void Start()
        {
            StartCoroutine(DestroyObject());
        }

        private IEnumerator DestroyObject()
        {
            yield return new WaitForSeconds(3);
        
            Destroy(gameObject);
        }
    }
}
