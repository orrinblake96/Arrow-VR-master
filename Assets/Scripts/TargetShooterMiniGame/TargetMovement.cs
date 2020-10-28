using System.Collections;
using UnityEngine;

namespace TargetShooterMiniGame
{
    public class TargetMovement : MonoBehaviour
    {
        private string _parentGameObjectName;

        private float _timeLower = 3f;
        private float _timeHigher = 4f;
        
        private void Start()
        {
            // get parents name
            _parentGameObjectName = gameObject.transform.parent.gameObject.name;

            switch (_parentGameObjectName)
            {
                case "SpawnPoint1":
                case "SpawnPoint8":
                    LeanTween.moveY(gameObject, Random.Range(5f, 7f), Random.Range(_timeLower, _timeHigher)).setLoopPingPong();
                    break;
                case "SpawnPoint10":
                {
                    float randomTime = Random.Range(_timeLower, _timeHigher);
                    LeanTween.moveLocalZ(gameObject, -2.02f, randomTime).setLoopPingPong();
                    LeanTween.moveLocalX(gameObject, 3.12f, randomTime).setLoopPingPong();
                    break;
                }
                case "SpawnPoint11":
                case "SpawnPoint20":
                {
                    LeanTween.moveLocalY(gameObject, Random.Range(5f, 7f), Random.Range(_timeLower, _timeHigher)).setLoopPingPong();
                    break;
                }
                case "SpawnPoint12":
                {
                    LeanTween.moveLocalX(gameObject, -3.08f, Random.Range(_timeLower, _timeHigher)).setLoopPingPong();
                    break;
                }
                case "SpawnPoint13":
                {
                    LeanTween.moveLocalX(gameObject, 3.08f, Random.Range(_timeLower, _timeHigher)).setLoopPingPong();
                    break;
                }
                case "SpawnPoint19":
                {
                    LeanTween.moveLocalY(gameObject, Random.Range(-3f, -4f), Random.Range(_timeLower, _timeHigher)).setLoopPingPong();
                    break;
                }
                case "SpawnPoint14":
                case "SpawnPoint15":
                case "SpawnPoint16":
                {
                    LeanTween.moveLocalX(gameObject, Random.Range(3f, 5f), Random.Range(_timeLower, _timeHigher)).setLoopPingPong();
                    break;
                }
                case "SpawnPoint9":
                {
                    float randomTime = Random.Range(_timeLower, _timeHigher);
                    LeanTween.moveLocalZ(gameObject, 2.4f, randomTime).setLoopPingPong();
                    LeanTween.moveLocalX(gameObject, -2.84f, randomTime).setLoopPingPong();
                    break;
                }
                case "SpawnPoint3":
                case "SpawnPoint5":
                case "SpawnPoint7":
                case "SpawnPoint17":
                case "SpawnPoint18":
                {
                    float randomTime = Random.Range(_timeLower, _timeHigher);
                    LeanTween.moveLocalZ(gameObject, 2.91f, randomTime).setLoopPingPong();
                    LeanTween.moveLocalX(gameObject, 3.55f, randomTime).setLoopPingPong();
                    break;
                }
                case "SpawnPoint4":
                case "SpawnPoint6":
                case "SpawnPoint2":
                {
                    float randomTime = Random.Range(_timeLower, _timeHigher);
                    LeanTween.moveLocalZ(gameObject, -3.19f, randomTime).setLoopPingPong();
                    LeanTween.moveLocalX(gameObject, -3.19f, randomTime).setLoopPingPong();
                    break;
                }
            }
        }
    }
}
