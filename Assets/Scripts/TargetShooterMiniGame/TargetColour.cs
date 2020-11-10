using System.Collections;
using UnityEngine;

namespace TargetShooterMiniGame
{
    public class TargetColour : MonoBehaviour
    {
        [SerializeField] private Material[] possibleColours;
        
        private Material _material;
        private void Awake()
        {
            GetComponent<MeshRenderer>().material = possibleColours[Random.Range(0, possibleColours.Length)];
        }
    }
}
