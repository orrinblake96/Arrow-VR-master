using System;
using UnityEngine;

namespace AllLevels
{
    public class InstructionLinePointers : MonoBehaviour
    {
        [SerializeField] private Transform lineRendererToPoint;
        
        private LineRenderer _lineRenderer;
        private void Start()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            _lineRenderer.useWorldSpace = false;
            
            _lineRenderer.SetPosition(1, gameObject.transform.InverseTransformPoint(lineRendererToPoint.position));
        }
    }
}
