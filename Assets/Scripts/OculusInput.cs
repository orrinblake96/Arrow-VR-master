using System;
using BowArrow;
using PillarOfLight;
using UnityEngine;

public class OculusInput : MonoBehaviour
{
    public Bow[] m_Bow = null;
    public GameObject[] bows;
    public GameObject[] customHands;
    public GameObject[] toolTipControllers;
    public GameObject[] m_OppositeController = null;
    public OVRInput.Controller[] m_Controller;

    [HideInInspector]
    public int dominantHandIndex = 0;

    private void Start()
    {
        OVRManager.fixedFoveatedRenderingLevel = OVRManager.FixedFoveatedRenderingLevel.Medium;
    }

    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, m_Controller[dominantHandIndex])) m_Bow[dominantHandIndex].Pull(m_OppositeController[dominantHandIndex].transform);

        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, m_Controller[dominantHandIndex]))
        {
            OVRInput.SetControllerVibration(0, 0, m_Controller[dominantHandIndex]);
            m_Bow[dominantHandIndex].Release();
        }
        
    }

    public void UpdateDominantBowHand()
    {
        if (PlayerPrefs.GetInt("dominantHandIndex") == 0)
        {
            dominantHandIndex = 1;
            bows[0].SetActive(false);
            bows[1].SetActive(true);
            customHands[0].SetActive(false);
            customHands[1].SetActive(true);
            toolTipControllers[0].SetActive(false);
            toolTipControllers[1].SetActive(true);
                
            PlayerPrefs.SetInt("dominantHandIndex", 1);
            PlayerPrefs.Save();
        }
        else
        {
            dominantHandIndex = 0;
            bows[0].SetActive(true);
            bows[1].SetActive(false);
            customHands[1].SetActive(false);
            customHands[0].SetActive(true);
            toolTipControllers[1].SetActive(false);
            toolTipControllers[0].SetActive(true);
                
            PlayerPrefs.SetInt("dominantHandIndex", 0);
            PlayerPrefs.Save();
        }
        
        GameObject.Find("Tip").GetComponent<ArrowTipColorChecker>().SetColourToCurrentMaterial();
    }
    
    public void GetDominantBowHand()
    {
        if (PlayerPrefs.GetInt("dominantHandIndex") == 1)
        {
            dominantHandIndex = 1;
            bows[0].SetActive(false);
            bows[1].SetActive(true);
            customHands[0].SetActive(false);
            customHands[1].SetActive(true);
            if (toolTipControllers.Length == 0) return;
            toolTipControllers[0].SetActive(false);
            toolTipControllers[1].SetActive(true);
        }
        else
        {
            dominantHandIndex = 0;
            bows[0].SetActive(true);
            bows[1].SetActive(false);
            customHands[1].SetActive(false);
            customHands[0].SetActive(true);
            if (toolTipControllers.Length == 0) return;
            toolTipControllers[1].SetActive(false);
            toolTipControllers[0].SetActive(true);
        }
    }
}