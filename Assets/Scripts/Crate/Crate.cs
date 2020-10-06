using UnityEngine;

namespace Crate
{
    public class Crate : MonoBehaviour, IDamageable
    {
        [Header("Switch Dominant Bow Hand")]
        public OculusInput oculusInput;
        
        [Header("Choose Game-Mode")]
        public GameObject destroyedCrate;
        public LevelManager levelManager;
        public string levelChosen;
        public string soundPath;
        
        [Header("Begin Game")]
        public GameObject[] gameModeSigns;
        public GameObject[] gameObjectsToHide;
        public GameObject destroyedShowGameModes;
        
        public void Damage(int amount)
        {
            DestroyCrate();
        }

        private void DestroyCrate()
        {
            if (levelChosen == "ChangeBowHand")
            {
                oculusInput.UpdateDominantBowHand();
                FMODUnity.RuntimeManager.PlayOneShot(soundPath, transform.position);
            }
            else if (levelChosen == "BeginGame")
            {
                FMODUnity.RuntimeManager.PlayOneShot(soundPath, transform.position);
                Instantiate(destroyedShowGameModes, transform.position, transform.rotation);
                Destroy(gameObject);
                
                foreach (GameObject sign in gameModeSigns)
                {
                    sign.SetActive(true);
                }
                
                foreach (GameObject objectsToHide in gameObjectsToHide)
                {
                    objectsToHide.SetActive(false);
                }
            }
            else
            {
                levelManager.StartSelectedGameMode(levelChosen);
                FMODUnity.RuntimeManager.PlayOneShot(soundPath, transform.position);
                Instantiate(destroyedCrate, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }
}
