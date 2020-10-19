using System.Collections;
using System.Collections.Generic;
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
        [SerializeField] private List<GameObject> easyTargetsMovingList;
        [SerializeField] private List<GameObject> mediumTargetsMovingList;
        [SerializeField] private List<GameObject> hardTargetsMovingList;
        
        private List<GameObject> _currentTargetList;
        private int _currentTargetListIndex = 0;
        private GameObject _nextPosition;
        private Transform _playerPosition;

        private void Start()
        {
            _playerPosition = GameObject.Find("OVRCameraRig").transform;
            _currentTargetList = easyTargetsList;
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
                    Debug.Log("Done");
                    break;
            }
        }

        private static IEnumerator ActivateTarget(GameObject target)
        {
            yield return new WaitForSeconds(1f);
            target.SetActive(true);
        }
    }
}
