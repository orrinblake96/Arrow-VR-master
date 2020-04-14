using _BowAndArrow.Scripts;
using _BowAndArrow.Scripts.Crate;
using UnityEngine;

namespace Crate
{
    public class Crate : MonoBehaviour, IDamageable
    {
        public GameObject destroyedCrate;

        public void Damage(int amount)
        {
            DestroyCrate();
        }

        private void DestroyCrate()
        {
            if (gameObject.name == "Wooden_Crate")
            {
                FindObjectOfType<AudioManager>().Play("WoodenBoxBreak");
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
