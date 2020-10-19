using System;
using UnityEngine;
using System.Collections;

namespace AccuracyMiniGame
{
    public class MoveTarget : MonoBehaviour
    {
        private void Start()
        {
            LeanTween.moveLocalY(gameObject, 10f, 3f).setLoopPingPong().setEaseInOutSine();
        }
    }
}
