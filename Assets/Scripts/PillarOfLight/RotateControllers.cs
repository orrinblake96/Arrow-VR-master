using System;
using System.Collections;
using UnityEngine;

namespace PillarOfLight
{
    public class RotateControllers : MonoBehaviour
    {
        private GameObject _rotationPoint;
        private bool _rotatingNow = false;

        private void Start()
        {
            _rotationPoint = GameObject.Find("octogonal-tower");
            StartCoroutine(RotatingControllers());
        }

        private void Update()
        {

            if (!_rotatingNow) return;
            
            transform.RotateAround(_rotationPoint.transform.position, Vector3.up, .3f);
        }

        private IEnumerator RotatingControllers()
        {
            yield return new WaitForSeconds(3f);
            _rotatingNow = true;
            yield return new WaitForSeconds(5f);
            _rotatingNow = false;
            this.enabled = false;
        }

        public bool RotatingNow
        {
            get => _rotatingNow;
            set => _rotatingNow = value;
        }
    }
}
