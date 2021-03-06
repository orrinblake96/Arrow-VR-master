﻿using System;
using BowArrow;
using FMODUnity;
using UnityEngine;
using UnityEngine.UI;

namespace PillarOfLight
{
    public class ArrowTipColorChecker : MonoBehaviour
    {
        public Material[] cubeColors;
        public ColoredCubeInfo colorInfoNum;
        public MeshRenderer shaftColor;
        public Arrow arrow;

        private MeshRenderer _tipColor;
        private SkinnedMeshRenderer _cubeColor;
        private Text _arrowInfoText;
        private Text _colorMatchText;
        private int _currentMaterial;
        private Material _currentMaterialColor;
        private Transform _enemyHitTransform;
        private StudioEventEmitter _colourChangeSoundEffect;

        private void Awake()
        {
            _tipColor = GetComponent<MeshRenderer>();

            colorInfoNum = GameObject.Find("EnemyManager").GetComponent<ColoredCubeInfo>();

            _colourChangeSoundEffect = GetComponent<StudioEventEmitter>();
        }
    
        private void Start()
        {
            SetColourToCurrentMaterial();
        }
    
        private void Update()
        {
            // If arrow has been fired then dont allow it to change colour
            if (arrow.arrowFired) return;
            
            if (OVRInput.GetDown(OVRInput.Button.One))
            {
                _colourChangeSoundEffect.Play();
                colorInfoNum.SetCurrentColor(Math.Abs((_currentMaterial += 1)) % 3);
                _currentMaterialColor = cubeColors[ colorInfoNum.currentMaterialPosition];
                _tipColor.material = _currentMaterialColor;
                shaftColor.material = _currentMaterialColor;
                return;
            }
            
            if (OVRInput.GetDown(OVRInput.Button.Two))
            {
                _colourChangeSoundEffect.Play();
                colorInfoNum.SetCurrentColor(Math.Abs((_currentMaterial -= 1)) % 3);
                _currentMaterialColor = cubeColors[ colorInfoNum.currentMaterialPosition];
                _tipColor.material = _currentMaterialColor;
                shaftColor.material = _currentMaterialColor;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            // If no skinned renderer exists then return
            if (other.gameObject.GetComponent<SkinnedMeshRenderer>() == null) return;
            if (other.gameObject.name == "LargeMonster")
            {
                other.transform.parent.GetComponent<DestroyLargeEnemy>().Damage(10);
            }
        
            //Check if cubes color matches tip of arrow
            _cubeColor = other.gameObject.GetComponent<SkinnedMeshRenderer>();
            if ((_cubeColor.material.color.r != _tipColor.material.color.r) ||
                (_cubeColor.material.color.g != _tipColor.material.color.g) ||
                (_cubeColor.material.color.b != _tipColor.material.color.b))
            {
                Destroy(transform.parent.parent.gameObject);
                return;
            }
            other.transform.parent.GetComponent<DestroyingEnemies>().Damage(10);
        }

        public void SetColourToCurrentMaterial()
        {
            _currentMaterial = colorInfoNum.currentMaterialPosition;
            _currentMaterialColor = cubeColors[ colorInfoNum.currentMaterialPosition];
            _tipColor.material = _currentMaterialColor;
            shaftColor.material = _currentMaterialColor;
        }

        public Material CurrentMaterialColor => _currentMaterialColor;
    }
}
