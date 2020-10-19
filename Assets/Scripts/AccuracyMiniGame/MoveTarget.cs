using System;
using UnityEngine;
using System.Collections;

namespace AccuracyMiniGame
{
    public class MoveTarget : MonoBehaviour
    {
        public void StartVerticalMovement()
        {
            LeanTween.moveLocalY(gameObject, 10f, 3f).setLoopPingPong().setEaseInOutSine();
        }
    }
}
