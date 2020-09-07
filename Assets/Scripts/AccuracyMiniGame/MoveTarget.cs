using UnityEngine;
using System.Collections;

namespace AccuracyMiniGame
{
    public class MoveTarget : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                MoveTargetAfterTargetDestroyed();
            }
        }

        public void MoveTargetAfterTargetDestroyed()
        {
            float newXPos = transform.position.x + 2;
            
            LeanTween.moveLocalX(gameObject, newXPos, 2);
        }
        
        public void HideTarget(GameObject target)
        {
            StartCoroutine(Hide(target));
        }

        private IEnumerator Hide(GameObject target)
        {
            target.SetActive(false);
            
            yield return new WaitForSeconds(2f);
            
            target.SetActive(true);
        }
    }
}
