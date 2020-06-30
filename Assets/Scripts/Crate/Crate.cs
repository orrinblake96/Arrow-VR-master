using _BowAndArrow.Scripts;
using _BowAndArrow.Scripts.Crate;
using UnityEngine;

namespace Crate
{
    public class Crate : MonoBehaviour, IDamageable
    {
        public GameObject destroyedCrate;
        public LevelManager levelManager;

        public void Damage(int amount)
        {
            DestroyCrate();
        }

        private void DestroyCrate()
        {
            switch (gameObject.name)
            {
                case "Crate":
                    print("---------------------------- Wave Game ----------------------------");
                    levelManager.StartSelectedGameMode("WaveBasedAltLayout");
                    break;
                case "MainMenuSign":
                    print("---------------------------- Main Menu Area ----------------------------");
                    levelManager.StartSelectedGameMode("MainMenuArea");
                    break;
            }
            FindObjectOfType<AudioManager>().Play("WoodenBoxBreak");
            Instantiate(destroyedCrate, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
