using FMODUnity;
using UnityEngine;

namespace TargetShooterMiniGame
{
    public class GameOver : MonoBehaviour
    {
        [SerializeField] private TargetDestructionTimer timer;

        [Header("Hideable/Showable Objects")]
        [SerializeField] private GameObject[] objectsToShow;
        [SerializeField] private GameObject[] objectsToHide;
        
        private bool _timerStopped;
        private StudioEventEmitter _gameOverSound;

        private void Start()
        {
            _gameOverSound = GetComponent<StudioEventEmitter>();
        }

        private void Update()
        {
            //When all targets are destroyed stop the timer
            if (gameObject.transform.childCount > 0 || _timerStopped) return;
            
            _gameOverSound.Play();
            _timerStopped = true;
            timer.StopTimer();
            ShowObjects();
            HideObjects();
        }

        private void HideObjects()
        {
            foreach (GameObject hideableObject in objectsToHide) hideableObject.SetActive(false);
        }
        
        private void ShowObjects()
        {
            foreach (GameObject showableObject in objectsToShow) showableObject.SetActive(true);
        }
    }
}
