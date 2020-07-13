using Crate;
using UnityEngine;

namespace TargetShooterMiniGame
{
    public class StartGame : MonoBehaviour
    {
        public GameObject startUi;
        public GameObject destroyableTargets;
        public TargetDestructionTimer timer;

        private void OnDestroy()
        {
            startUi.SetActive(false);
            destroyableTargets.SetActive(true);
            timer.enabled = true;
        }
    }
}
