using System;
using FMODUnity;
using UnityEngine;

namespace TargetShooterMiniGame
{
    public class CountDownTimerGameOver : MonoBehaviour
    {
        [Header("Hideable/Showable Objects")]
        [SerializeField] private GameObject[] objectsToShow;
        [SerializeField] private GameObject[] objectsToHide;
        
        [Header("Game Over Sounds")]
        [SerializeField] private string winSoundPath;
        [SerializeField] private string loseSoundPath;
        
        private Animator _scorePulsate;
        private static readonly int GameOver = Animator.StringToHash("GameOver");

        private void Start()
        {
            _scorePulsate = GameObject.Find("CountDownTimerCanvas").GetComponent<Animator>();
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

        public void EndGame(bool timerAtZero)
        {
            _scorePulsate.SetBool(GameOver, true);
            
            if (!timerAtZero)
            {
                FMODUnity.RuntimeManager.PlayOneShot(winSoundPath, transform.position);
                HideObjects();
                ShowObjects();
                return;
            }
            
            FMODUnity.RuntimeManager.PlayOneShot(loseSoundPath, transform.position);
            HideObjects();
            ShowObjects();
        }
    }
}
