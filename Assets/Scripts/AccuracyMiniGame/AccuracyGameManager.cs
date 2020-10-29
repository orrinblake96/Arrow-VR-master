using System.Collections;
using System.Collections.Generic;
using AllLevels.HighScore;
using FMODUnity;
using TargetShooterMiniGame;
using UnityEngine;

namespace AccuracyMiniGame
{
    public class AccuracyGameManager : MonoBehaviour
    {
        [Header("Static Targets")]
        [SerializeField] private List<GameObject> easyTargetsList;
        [SerializeField] private List<GameObject> mediumTargetsList;
        [SerializeField] private List<GameObject> hardTargetsList;
        
        [Header("Moving Targets")]
        [SerializeField] private MoveTarget _moveTarget;
        [SerializeField] private List<GameObject> easyTargetsMovingList;
        [SerializeField] private List<GameObject> mediumTargetsMovingList;
        [SerializeField] private List<GameObject> hardTargetsMovingList;

        [Header("Objects To Show/Hide")] 
        [SerializeField] private GameObject[] objectsToShow;
        [SerializeField] private GameObject[] objectsToHide;
        
        private List<GameObject> _currentTargetList;
        private int _currentTargetListIndex = 0;
        private GameObject _nextPosition;
        private Transform _playerPosition;
        
        private TargetScore _targetScore;
        private ScoreboardEntryData _entryData = new ScoreboardEntryData();
        private Scoreboard _highscoreBoard;
        private Animator _scoreAnimator;
        private static readonly int GameOver = Animator.StringToHash("GameOver");
        private StudioEventEmitter _gameOverSound;

        private void Start()
        {
            _playerPosition = GameObject.Find("OVRCameraRig").transform;
            _currentTargetList = easyTargetsList;
            
            _targetScore = GameObject.Find("ScorenumberTMP").GetComponent<TargetScore>();
            _scoreAnimator = GameObject.Find("PlayerScoreCanvasUI").GetComponent<Animator>();

            _gameOverSound = GetComponent<StudioEventEmitter>();
        }

        public void DisplayNextTarget(GameObject target)
        {
            if (_currentTargetList.Count <= 0)
            {
                _currentTargetListIndex++;
                GetNextTargetList(target);
                return;
            }
            
            _nextPosition = GetNextTarget();

            target.transform.position = _nextPosition.transform.position;
            target.transform.rotation = _nextPosition.transform.rotation;

            StartCoroutine(ActivateTarget(target));

            _currentTargetList.Remove(_nextPosition);
        }

        private GameObject GetNextTarget()
        {
            return _currentTargetList[Random.Range(0, _currentTargetList.Count)];
        }

        private void GetNextTargetList(GameObject target)
        {
            switch (_currentTargetListIndex)
            {
                case 1:
                    _currentTargetList = mediumTargetsList;
                    DisplayNextTarget(target);
                    break;
                case 2:
                    _currentTargetList = hardTargetsList;
                    DisplayNextTarget(target);
                    break;
                case 3:
                    _currentTargetList = easyTargetsMovingList;
                    _moveTarget.StartVerticalMovement();
                    DisplayNextTarget(target);
                    break;
                case 4:
                    _currentTargetList = mediumTargetsMovingList;
                    DisplayNextTarget(target);
                    break;
                case 5:
                    _currentTargetList = hardTargetsMovingList;
                    DisplayNextTarget(target);
                    break;
                case 6:
                    EndGame();
                    break;
            }
        }

        private void EndGame()
        {
            _gameOverSound.Play();
            PlayScoreAnimation();
            ObjectsToHide();
            ObjectsToShow();
            HighscoreHandler();
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

        private void HighscoreHandler()
        {
            _highscoreBoard = GameObject.Find("ScoreBoard").GetComponent<Scoreboard>();
            _entryData.entryName = "Player";
            _entryData.entryScore = _targetScore.CurrentScore;
            _highscoreBoard.AddEntry(_entryData);
        }

        private void PlayScoreAnimation()
        {
            _scoreAnimator.SetBool(GameOver, true);
        }

        private static IEnumerator ActivateTarget(GameObject target)
        {
            yield return new WaitForSeconds(1f);
            target.SetActive(true);
        }
    }
}
