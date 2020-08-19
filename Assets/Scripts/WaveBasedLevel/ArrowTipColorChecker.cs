using PillarOfLight;
using UnityEngine.UI;
using UnityEngine;
using WaveBasedLevel;

public class ArrowTipColorChecker : MonoBehaviour
{
    public Material[] cubeColors;
    public ColoredCubeInfo colorInfoNum;
    public MeshRenderer shaftColor;
    public GameObject fracturedEnemyRed, fracturedEnemyGreen, fracturedEnemyBlue;
    public GameObject[] explosionParticles;

    private MeshRenderer _tipColor;
    private SkinnedMeshRenderer _cubeColor;
    private Text _arrowInfoText;
    private Text _colorMatchText;
    private int _currentMaterial;
    private Material _currentMaterialColor;
    private Transform _enemyHitTransform;
    private WaveScore _waveScoreBoard;
    private bool _scoreBoardExists = false;
    private SpecialAbilitiesBar _specialAbilitiesBar;

    private void Awake()
    {
        _tipColor = GetComponent<MeshRenderer>();

        // Check if the GO exists, for main menu usage
        if (GameObject.FindGameObjectWithTag("WaveScoreBoard") != null)
        {
            _waveScoreBoard = GameObject.FindGameObjectWithTag("WaveScoreBoard").GetComponent<WaveScore>();
            _specialAbilitiesBar = GameObject.Find("AbilitiesSlider").GetComponent<SpecialAbilitiesBar>();
            _scoreBoardExists = true;
        }

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
        //Check if cubes color matches tip of arrow
        _cubeColor = other.gameObject.GetComponent<SkinnedMeshRenderer>();
        
        //*************************** Refine **********************************
        if ((_cubeColor.material.color.r == _tipColor.material.color.r) && 
            (_cubeColor.material.color.g == _tipColor.material.color.g) && 
            (_cubeColor.material.color.b == _tipColor.material.color.b))
        {
            _enemyHitTransform = other.gameObject.transform;
            
            // Instantiate correct particle colours
            if (_cubeColor.material.color.r == 1)
            {
                Instantiate(explosionParticles[0], transform.position + Vector3.up, transform.rotation);
            }
            if (_cubeColor.material.color.g == 1)
            {
                Instantiate(explosionParticles[1], transform.position + Vector3.up, transform.rotation);
            }
            if (_cubeColor.material.color.b == 1)
            {
                Instantiate(explosionParticles[2], transform.position + Vector3.up, transform.rotation);
            }
            
            // Increase score, power and destroy enemy
            if(_scoreBoardExists) _waveScoreBoard.IncreaseCurrentScore();
            _specialAbilitiesBar.IncrementPower(.05f);
            Destroy(_enemyHitTransform.parent.gameObject);
        }
    }
}
