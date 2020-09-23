using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AllLevels
{
    public class CameraFacingBillboard : MonoBehaviour
    {
        private Camera _mainCamera;

        private void Start()
        {
            _mainCamera = GameObject.Find("CenterEyeAnchor").GetComponent<Camera>();
            LeanTween.moveLocalY(gameObject, Random.Range(transform.position.y + 3f, transform.position.y + 1.5f), Random.Range(1f, 2f));
            LeanTween.scale(gameObject, Vector3.zero, Random.Range(1f, 2f));
        }

        //Orient the camera after all movement is completed this frame to avoid jittering
        void LateUpdate()
        {
            transform.LookAt(transform.position + _mainCamera.transform.rotation * Vector3.forward,
                _mainCamera.transform.rotation * Vector3.up);
        }
    }
}
