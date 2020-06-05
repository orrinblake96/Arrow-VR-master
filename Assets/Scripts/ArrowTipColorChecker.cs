using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArrowTipColorChecker : MonoBehaviour
{
    public Material[] cubeColors;
    public ColoredCubeInfo colorInfoNum;
    public MeshRenderer shaftColor;
    public GameObject fracturedEnemyRed, fracturedEnemyGreen, fracturedEnemyBlue;

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
//        _arrowInfoText = GameObject.Find("Canvas/ArrowInfoText").GetComponent<Text>();
//        _colorMatchText = GameObject.Find("Canvas/ColorMatch").GetComponent<Text>();

        colorInfoNum = GameObject.Find("EnemyManager").GetComponent<ColoredCubeInfo>();
    }

    // Start is called before the first frame update
    private void Start()
    {
//        _arrowInfoText.text = "Arrow Color: " + _tipColor.material.color.ToString();

        _currentMaterial = colorInfoNum.currentMaterialPosition;
        _currentMaterialColor = cubeColors[ colorInfoNum.currentMaterialPosition];
        _tipColor.material = _currentMaterialColor;
        shaftColor.material = _currentMaterialColor;
    }

    // Update is called once per frame
    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.One) || Input.GetKeyDown(KeyCode.A))
        {
            colorInfoNum.SetCurrentColor((_currentMaterial += 1) % 3);
//            _currentMaterialColor = cubeColors[(_currentMaterial++) % 3];
            _currentMaterialColor = cubeColors[ colorInfoNum.currentMaterialPosition];
            _tipColor.material = _currentMaterialColor;
            shaftColor.material = _currentMaterialColor;
//            _arrowInfoText.text = "Arrow Color: " + _tipColor.material.color.ToString();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Check if cubes color matches tip of arrow
        _cubeColor = other.gameObject.GetComponent<SkinnedMeshRenderer>();
        if ((_cubeColor.material.color.r == _tipColor.material.color.r) && 
            (_cubeColor.material.color.g == _tipColor.material.color.g) && 
            (_cubeColor.material.color.b == _tipColor.material.color.b))
        {
            _enemyHitTransform = other.gameObject.transform;
            
            if (_cubeColor.material.color.r == 1)
            {
                Instantiate(fracturedEnemyRed, transform.position + Vector3.up, transform.rotation);   
            }
            if (_cubeColor.material.color.g == 1)
            {
                Instantiate(fracturedEnemyGreen, transform.position + Vector3.up, transform.rotation);   
            }
            if (_cubeColor.material.color.b == 1)
            {
                Instantiate(fracturedEnemyBlue, transform.position + Vector3.up, transform.rotation);   
            }
            
            Destroy(_enemyHitTransform.parent.gameObject);
        }
    }
}
