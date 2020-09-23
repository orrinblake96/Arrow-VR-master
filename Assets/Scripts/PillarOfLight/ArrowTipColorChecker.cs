using BowArrow;
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

        private void Awake()
        {
            _tipColor = GetComponent<MeshRenderer>();

            colorInfoNum = GameObject.Find("EnemyManager").GetComponent<ColoredCubeInfo>();
        }
    
        private void Start()
        {
            _currentMaterial = colorInfoNum.currentMaterialPosition;
            _currentMaterialColor = cubeColors[ colorInfoNum.currentMaterialPosition];
            _tipColor.material = _currentMaterialColor;
            shaftColor.material = _currentMaterialColor;
        }
    
        private void Update()
        {
            // If arrow has been fired then dont allow it to change colour
            if (arrow.arrowFired) return;
            
            if (OVRInput.GetDown(OVRInput.Button.One) || Input.GetKeyDown(KeyCode.A))
            {
                colorInfoNum.SetCurrentColor((_currentMaterial += 1) % 3);
                _currentMaterialColor = cubeColors[ colorInfoNum.currentMaterialPosition];
                _tipColor.material = _currentMaterialColor;
                shaftColor.material = _currentMaterialColor;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<SkinnedMeshRenderer>() == null) return;
            
            if (other.gameObject.name == "LargeMonster")
            {
                other.transform.parent.GetComponent<DestroyLargeEnemy>().Damage(10);
            }
        
            //Check if cubes color matches tip of arrow
            _cubeColor = other.gameObject.GetComponent<SkinnedMeshRenderer>();
            
            //*************************** Refine **********************************
            if ((_cubeColor.material.color.r != _tipColor.material.color.r) ||
                (_cubeColor.material.color.g != _tipColor.material.color.g) ||
                (_cubeColor.material.color.b != _tipColor.material.color.b)) return;
        
//            _enemyHitTransform = other.gameObject.transform;
//
//            Destroy(_enemyHitTransform.parent.gameObject);
            other.transform.parent.GetComponent<DestroyingEnemies>().Damage(10);
        }
    }
}
