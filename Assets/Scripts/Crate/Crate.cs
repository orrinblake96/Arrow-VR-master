using Audio;
using UnityEngine;

namespace Crate
{
    public class Crate : MonoBehaviour, IDamageable
    {
        public GameObject destroyedCrate;
        public LevelManager levelManager;
        public string levelChosen;

        public void Damage(int amount)
        {
            DestroyCrate();
        }

        private void DestroyCrate()
        {
            levelManager.StartSelectedGameMode(levelChosen);
            FindObjectOfType<AudioManager>().Play("WoodenBoxBreak");
            Instantiate(destroyedCrate, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
