using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArrowTipColorChecker : MonoBehaviour
{
    public Material[] cubeColors;
    public GameObject newCube;
    
    private MeshRenderer _tipColor;
    private MeshRenderer _cubeColor;
    private Text _arrowInfoText;
    private Text _colorMatchText;
    private int _currentMaterial = 0;

    private void Awake()
    {
        _tipColor = GetComponent<MeshRenderer>();
        _arrowInfoText = GameObject.Find("Canvas/ArrowInfoText").GetComponent<Text>();
        _colorMatchText = GameObject.Find("Canvas/ColorMatch").GetComponent<Text>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        _arrowInfoText.text = "Arrow Color: " + _tipColor.material.color.ToString();
    }

    // Update is called once per frame
    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            _tipColor.material = cubeColors[(_currentMaterial++) % 3];
            _arrowInfoText.text = "Arrow Color: " + _tipColor.material.color.ToString();
        }
        
        //Reset scene for quick testing
        if (OVRInput.GetDown(OVRInput.Button.Three))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Check if cubes color matches tip of arrow
        _cubeColor = other.gameObject.GetComponent<MeshRenderer>();
        if ((_cubeColor.material.color.r == _tipColor.material.color.r) && 
            (_cubeColor.material.color.g == _tipColor.material.color.g) && 
            (_cubeColor.material.color.b == _tipColor.material.color.b))
        {
            _colorMatchText.text = "Color Matched: true";
            Destroy(other.gameObject);
        }
        else
        {
            _colorMatchText.text = "Color Matched: false";
        }
    }
}
