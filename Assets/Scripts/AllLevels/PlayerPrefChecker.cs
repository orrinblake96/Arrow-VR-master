using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AllLevels
{
    public class PlayerPrefChecker : MonoBehaviour
    {
        [SerializeField] private GameObject[] arrowShootingToolTips;
        [SerializeField] private OculusInput dominantHandIndex;
        private void Awake()
        {
            if (SceneManager.GetActiveScene().name == "MainMenuArea" && PlayerPrefs.GetInt("ArrowShotOnce") == 1)
            {
                foreach (GameObject toolTip in arrowShootingToolTips)
                {
                    toolTip.SetActive(false);
                }
            }
            
            if (PlayerPrefs.HasKey("dominantHandIndex"))
            {
                dominantHandIndex.GetDominantBowHand();
                return;
            }
            
            PlayerPrefs.SetInt("dominantHandIndex", 0);
            PlayerPrefs.Save();
        }
    }
}
