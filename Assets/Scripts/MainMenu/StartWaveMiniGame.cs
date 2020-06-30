using UnityEngine;

namespace MainMenu
{
    public class StartWaveMiniGame : MonoBehaviour
    {
        public LevelManager levelManager;

        private void OnTriggerEnter(Collider other)
        {
            print("Starting Coroutine -----------------------------");
            levelManager.StartSelectedGameMode("WaveBasedAltLayout");
            Destroy(gameObject);
        }
    }
}