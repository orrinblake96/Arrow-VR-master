using System.Collections;
using UnityEngine;

namespace TargetShooterMiniGame
{
    public class TargetMovement : MonoBehaviour
    {
        private string _parentGameObjectName;
        private void Start()
        {
            // get parents name
            _parentGameObjectName = gameObject.transform.parent.gameObject.name;

            switch (_parentGameObjectName)
            {
                case "SpawnPoint1":
                case "SpawnPoint8":
                    LeanTween.moveY(gameObject, Random.Range(5f, 7f), Random.Range(2f, 4f)).setLoopPingPong();
                    break;
                case "SpawnPoint10":
                {
                    float randomTime = Random.Range(2f, 4f);
                    LeanTween.moveLocalZ(gameObject, -2.02f, randomTime).setLoopPingPong();
                    LeanTween.moveLocalX(gameObject, 3.12f, randomTime).setLoopPingPong();
                    break;
                }
                case "SpawnPoint9":
                {
                    float randomTime = Random.Range(2f, 4f);
                    LeanTween.moveLocalZ(gameObject, 2.4f, randomTime).setLoopPingPong();
                    LeanTween.moveLocalX(gameObject, -2.84f, randomTime).setLoopPingPong();
                    break;
                }
                case "SpawnPoint3":
                case "SpawnPoint5":
                case "SpawnPoint7":
                {
                    float randomTime = Random.Range(2f, 4f);
                    LeanTween.moveLocalZ(gameObject, 2.91f, randomTime).setLoopPingPong();
                    LeanTween.moveLocalX(gameObject, 3.55f, randomTime).setLoopPingPong();
                    break;
                }
                case "SpawnPoint4":
                case "SpawnPoint6":
                case "SpawnPoint2":
                {
                    float randomTime = Random.Range(2f, 4f);
                    LeanTween.moveLocalZ(gameObject, -3.19f, randomTime).setLoopPingPong();
                    LeanTween.moveLocalX(gameObject, -3.19f, randomTime).setLoopPingPong();
                    break;
                }
            }
        }
    }
}
