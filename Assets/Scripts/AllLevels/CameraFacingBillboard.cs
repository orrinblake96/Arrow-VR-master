using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AllLevels
{
    public class CameraFacingBillboard : MonoBehaviour
    {
        private Camera _mainCamera;

        private void OnEnable()
        {
            _mainCamera = GameObject.Find("CenterEyeAnchor").GetComponent<Camera>();
            LeanTween.moveLocalY(gameObject, transform.position.y + 5f, 2f);
            LeanTween.scale(gameObject, Vector3.zero, 2f);
        }

        private void OnDisable()
        {
            LeanTween.scale(gameObject, new Vector3(0.6f, 0.6f, 0.6f), 0f);
        }

        //Orient the camera after all movement is completed this frame to avoid jittering
        void LateUpdate()
        {
            transform.LookAt(transform.position + _mainCamera.transform.rotation * Vector3.forward,
                _mainCamera.transform.rotation * Vector3.up);
        }
    }
}
