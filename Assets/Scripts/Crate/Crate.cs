using UnityEngine;

namespace Crate
{
    public class Crate : MonoBehaviour, IDamageable
    {
        public OculusInput oculusInput;
        public GameObject destroyedCrate;
        public LevelManager levelManager;
        public string levelChosen;
        public string soundPath;
        
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
