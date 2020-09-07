using System.Collections;
using Crate;
using UnityEngine;

namespace AccuracyMiniGame
{
    public class DestroyAccuracyTarget : MonoBehaviour, IDamageable
    {
        private MoveTarget _moveTarget;
        
        private void Start()
        {
            _moveTarget = GameObject.Find("Target").GetComponent<MoveTarget>();
        }

        public void Damage(int amount)
        {
            _moveTarget.MoveTargetAfterTargetDestroyed();
            _moveTarget.HideTarget(gameObject);
        }
    }
}
