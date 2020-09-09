using System;
using UnityEngine;

namespace AllLevels
{
    public class HoverFloatingIslands : MonoBehaviour
    {
        private void Start()
        {
            LeanTween.moveLocalY(gameObject, 4f, 8f).setLoopPingPong().setEase(LeanTweenType.easeInOutQuad);
        }
    }
}
