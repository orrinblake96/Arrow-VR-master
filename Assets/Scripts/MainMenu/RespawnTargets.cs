using System.Collections;
using UnityEngine;

namespace MainMenu
{
    public class RespawnTargets : MonoBehaviour
    {
        public GameObject target;
        public Transform spawnPoint;

        private bool _waitingToSpawn = false;

        private void Update()
        {
            if (!IsDestroyed() || _waitingToSpawn) return;

            _waitingToSpawn = true;
            
            StartCoroutine(RespawnTarget());
        }

        private bool IsDestroyed()
        {
            return gameObject.transform.childCount <= 0;
        }

        IEnumerator RespawnTarget()
        {
            yield return new WaitForSeconds(2f);
            GameObject newTarget = Instantiate(target, spawnPoint.position, spawnPoint.rotation);
            newTarget.transform.SetParent(transform);
            _waitingToSpawn = false;
        }
    }
}
