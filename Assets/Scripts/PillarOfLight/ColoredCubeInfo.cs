﻿using UnityEngine.UI;
using UnityEngine;

public class ColoredCubeInfo : MonoBehaviour
{
    public int currentMaterialPosition;
    private Text _posText;

    private void Awake()
    {
//        _posText = GameObject.Find("Canvas/Pos").GetComponent<Text>();
    }

    private void Start()
    {
//        _posText.text = "Arrow Color Array Pos: " + currentMaterialPosition;
    }

    public void SetCurrentColor(int materialPosition)
    {
        currentMaterialPosition = materialPosition;
//        GameObject.Find("Canvas/Pos").GetComponent<Text>().text = "Arrow Color Array Pos: " + currentMaterialPosition;
    }
}
