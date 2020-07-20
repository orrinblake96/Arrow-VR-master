using System;
using BowArrow;
using UnityEngine;

public class OculusInput : MonoBehaviour
{
    public Bow[] m_Bow = null;
    public GameObject[] bows;
    public GameObject[] customHands;
    public GameObject[] m_OppositeController = null;
    public OVRInput.Controller[] m_Controller;

    [HideInInspector]
    public int dominantHandIndex = 0;

    private bool _paused;

    private void Start()
    {
        if (PlayerPrefs.HasKey("dominantHandIndex"))
        {
            GetDominantBowHand();
            return;
        }
        
        PlayerPrefs.SetInt("dominantHandIndex", 0);
        PlayerPrefs.Save();
    }

    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, m_Controller[dominantHandIndex])) m_Bow[dominantHandIndex].Pull(m_OppositeController[dominantHandIndex].transform);

        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, m_Controller[dominantHandIndex]))
        {
            m_Bow[dominantHandIndex].Release();
            OVRInput.SetControllerVibration(0, 0, m_Controller[dominantHandIndex]);
        }

        if (OVRInput.GetDown(OVRInput.Button.Start))
        {
            if (_paused)
            {
                Time.timeScale = 1;
                _paused = false;
            }
            else
            {
                Time.timeScale = 0;
                _paused = true;
            }
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
                
            PlayerPrefs.SetInt("dominantHandIndex", 1);
            PlayerPrefs.Save();
            print("Changed");
        }
        else
        {
            dominantHandIndex = 0;
            bows[0].SetActive(true);
            bows[1].SetActive(false);
            customHands[1].SetActive(false);
            customHands[0].SetActive(true);
                
            PlayerPrefs.SetInt("dominantHandIndex", 0);
            PlayerPrefs.Save();
            print("Changed");
        }
    }
    
    private void GetDominantBowHand()
    {
        if (PlayerPrefs.GetInt("dominantHandIndex") == 1)
        {
            dominantHandIndex = 1;
            bows[0].SetActive(false);
            bows[1].SetActive(true);
            customHands[0].SetActive(false);
            customHands[1].SetActive(true);
            print("Changed");
        }
        else
        {
            dominantHandIndex = 0;
            bows[0].SetActive(true);
            bows[1].SetActive(false);
            customHands[1].SetActive(false);
            customHands[0].SetActive(true);
            print("Changed");
        }
    }
}