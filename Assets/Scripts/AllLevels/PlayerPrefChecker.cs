using UnityEngine;
using UnityEngine.SceneManagement;

namespace AllLevels
{
    public class PlayerPrefChecker : MonoBehaviour
    {
        [SerializeField] private GameObject[] arrowShootingToolTips;
        [SerializeField] private GameObject[] signsToShow;
        
        private OculusInput _dominantHandIndex;

        private void Awake()
        {
            _dominantHandIndex = GameObject.Find("Input").GetComponent<OculusInput>();
        }

        private void Start()
        {
            if (SceneManager.GetActiveScene().name == "MainMenuArea" && PlayerPrefs.GetInt("ArrowShotOnce") == 1)
            {
                foreach (GameObject toolTip in arrowShootingToolTips)
                {
                    toolTip.SetActive(false);
                }
                
                foreach (GameObject sign in signsToShow)
                {
                    sign.SetActive(true);
                }
            }
            
            if (PlayerPrefs.HasKey("dominantHandIndex"))
            {
                _dominantHandIndex.GetDominantBowHand();
                return;
            }
            
            PlayerPrefs.SetInt("dominantHandIndex", 0);
            PlayerPrefs.Save();
        }
    }
}
