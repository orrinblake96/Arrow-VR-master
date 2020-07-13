using System;
using UnityEngine;

namespace TargetShooterMiniGame
{
    public class DestroyParticles : MonoBehaviour
    {
        private void Start()
        {
            Destroy(gameObject, 5f);
        }
    }
}
