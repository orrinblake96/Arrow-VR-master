using System;
using FMODUnity;
using UnityEngine;

namespace MainMenu
{
    public class WaterTriggerSound : MonoBehaviour
    {
        private StudioEventEmitter _eventEmitter;

        private void Awake()
        {
            _eventEmitter = GetComponent<StudioEventEmitter>();
        }

        private void OnTriggerEnter(Collider other)
        {
            print("Name: " + other.gameObject.name);
            if (other.gameObject.name == "Tip") _eventEmitter.Play();
        }
    }
}
