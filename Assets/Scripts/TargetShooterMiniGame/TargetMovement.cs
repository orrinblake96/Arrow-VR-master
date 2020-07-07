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

            if(_parentGameObjectName == "SpawnPoint1" || _parentGameObjectName == "SpawnPoint8")
            {
                LeanTween.moveY(gameObject, Random.Range(5f, 7f), Random.Range(2f, 4f)).setLoopPingPong();
            }
            else if(_parentGameObjectName == "SpawnPoint10")
            {
                float randomTime = Random.Range(2f, 4f);
                LeanTween.moveLocalZ(gameObject, -2.02f, randomTime).setLoopPingPong();
                LeanTween.moveLocalX(gameObject, 3.12f, randomTime).setLoopPingPong();
            } 
            else if(_parentGameObjectName == "SpawnPoint9")
            {
                float randomTime = Random.Range(2f, 4f);
                LeanTween.moveLocalZ(gameObject, 2.4f, randomTime).setLoopPingPong();
                LeanTween.moveLocalX(gameObject, -2.84f, randomTime).setLoopPingPong(); 
            } 
            else if (_parentGameObjectName == "SpawnPoint3" || _parentGameObjectName == "SpawnPoint5" || _parentGameObjectName == "SpawnPoint7")
            {
                float randomTime = Random.Range(2f, 4f);
                LeanTween.moveLocalZ(gameObject, 2.91f, randomTime).setLoopPingPong();
                LeanTween.moveLocalX(gameObject, 3.55f, randomTime).setLoopPingPong(); 
            } 
            else if (_parentGameObjectName == "SpawnPoint4" || _parentGameObjectName == "SpawnPoint6" || _parentGameObjectName == "SpawnPoint2")
            {
                float randomTime = Random.Range(2f, 4f);
                LeanTween.moveLocalZ(gameObject, -3.19f, randomTime).setLoopPingPong();
                LeanTween.moveLocalX(gameObject, -3.19f, randomTime).setLoopPingPong(); 
            }
            
        }
    }
}
