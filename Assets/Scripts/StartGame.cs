using System;
using UnityEngine;

namespace WaveBasedLevel
{
    public class StartGame : MonoBehaviour
    {
        [Header("Object To Show/Hide")]
        [SerializeField] private GameObject[] objectsToHide;
        [SerializeField] private GameObject[] objectsToShow;

        private void Update()
        {
            if (gameObject.transform.childCount > 0) return;
                ObjectsToShow();
                ObjectsToHide();
        }

        private void ObjectsToHide()
        {
            foreach (GameObject hideable in objectsToHide)
            {
                hideable.SetActive(false);
            }
        }

        private void ObjectsToShow()
        {
            foreach (GameObject showable in objectsToShow)
            {
                showable.SetActive(true);
            }
        }
    }
}
