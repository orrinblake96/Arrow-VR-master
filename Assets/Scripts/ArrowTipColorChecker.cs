using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ArrowTipColorChecker : MonoBehaviour
{
    private MeshRenderer _tipColor;
    private Text _arrowInfoText;
    private Text _colorMatchText;
    private Text _enemyDestroyedText;

    private void Awake()
    {
        _tipColor = GetComponent<MeshRenderer>();
        _arrowInfoText = GameObject.Find("Canvas/ArrowInfoText").GetComponent<Text>();
        _colorMatchText = GameObject.Find("Canvas/ColorMatch").GetComponent<Text>();
        _enemyDestroyedText = GameObject.Find("Canvas/EnemyDestroyed").GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _arrowInfoText.text = "Arrow Color: " + _tipColor.material.color.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (_tipColor.material.color.r == 1.0f)
        {
            _colorMatchText.text = "Color Matched: true";
        }
        else
        {
            _colorMatchText.text = "Color Matched: false";
        }

        if (OVRInput.GetDown(OVRInput.Button.One))
        {
//            _enemyDestroyedText.text = "Enemy Dead: True";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("RedEnemy"))
        {
            _enemyDestroyedText.text = "Enemy Dead: True";
            Destroy(other.gameObject);
        }
    }
}
