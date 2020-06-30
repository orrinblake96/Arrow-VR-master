using UnityEngine;

namespace MainMenu
{
    public class ReturnToMainMenu : MonoBehaviour
    {
        public LevelManager levelManager;

        private void OnTriggerEnter(Collider other)
        {
            levelManager.StartSelectedGameMode("MainMenuArea");
            Destroy(gameObject);
        }
    }
}
