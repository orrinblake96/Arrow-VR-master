using FMODUnity;
using UnityEngine;

namespace TargetShooterMiniGame
{
    public class CountDownTimerGameOver : MonoBehaviour
    {
        [Header("Hideable/Showable Objects")]
        [SerializeField] private GameObject[] objectsToShow;
        [SerializeField] private GameObject[] objectsToHide;
        
        private StudioEventEmitter _gameOverSound;
        
        private void Start()
        {
            _gameOverSound = GetComponent<StudioEventEmitter>();
        }
        private void HideObjects()
        {
            foreach (GameObject hideableObject in objectsToHide)
            {
                if (hideableObject == null) continue;
                hideableObject.SetActive(false);
            }
        }
        
        private void ShowObjects()
        {
            foreach (GameObject showableObject in objectsToShow) showableObject.SetActive(true);
        }

        public void EndGame()
        {
            _gameOverSound.Play();
            HideObjects();
            ShowObjects();
        }
    }
}
