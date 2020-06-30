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
            if (gameObject.name == "Crate")
            {
                FindObjectOfType<AudioManager>().Play("WoodenBoxBreak");
                print("Starting Wave Game----------------------------");
                levelManager.StartSelectedGameMode("WaveBasedAltLayout");
            }
            else if((gameObject.name == "Whisky_Bottle"))
            {
                FindObjectOfType<AudioManager>().Play("GlassSmash");
            }
            Instantiate(destroyedCrate, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
