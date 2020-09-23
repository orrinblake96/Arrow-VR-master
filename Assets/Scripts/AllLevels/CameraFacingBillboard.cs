using System;
using UnityEngine;

namespace AllLevels
{
    public class CameraFacingBillboard : MonoBehaviour
    {
        private Camera _mainCamera;

        private void Start()
        {
            _mainCamera = GameObject.Find("CenterEyeAnchor").GetComponent<Camera>();
        }

        //Orient the camera after all movement is completed this frame to avoid jittering
        void LateUpdate()
        {
            transform.LookAt(transform.position + _mainCamera.transform.rotation * Vector3.forward,
                _mainCamera.transform.rotation * Vector3.up);
        }
    }
}
