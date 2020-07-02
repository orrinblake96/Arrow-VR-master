using System;
using System.Collections;
using UnityEngine;

namespace TargetShooterMiniGame
{
    public class SpawnTargets : MonoBehaviour
    {
        public GameObject target;
        public float timeBetweenSpawns = 1f;

        private bool _waitingToSpawn = false;
        private void Update()
        {
            if (!IsDestroyed() || _waitingToSpawn) return;

            _waitingToSpawn = true; 
            
            StartCoroutine(SpawnNewTarget());
        }

        private bool IsDestroyed()
        {
            return gameObject.transform.childCount <= 0;
        }

        private IEnumerator SpawnNewTarget()
        {
            yield return new WaitForSeconds(timeBetweenSpawns);
            GameObject newTarget = Instantiate(target, transform.position, transform.rotation);
            newTarget.transform.SetParent(transform);
            _waitingToSpawn = false;
        }
    }
}
