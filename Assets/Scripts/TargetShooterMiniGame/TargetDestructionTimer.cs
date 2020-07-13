using UnityEngine;
using TMPro;

namespace TargetShooterMiniGame
{
    public class TargetDestructionTimer : MonoBehaviour
    {
        private TextMeshProUGUI _timerText;
        private string _seconds;
        private string _minutes;
        private bool _isPlaying;

        [HideInInspector]
        public float time;
        public float timeSpeed;

        private void Start()
        {
            _isPlaying = true;
            _timerText = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            //If timer is stopped, don't continue to increment
            if (!_isPlaying) return;
            
            //Timer can be speed up for testing
            time += Time.deltaTime * timeSpeed;
           
            //Simple conversion for formatting & display
            _seconds = (time % 60).ToString("00");
            _minutes = Mathf.Floor((time % 3600) / 60).ToString("00");
            
            _timerText.text = _minutes + ":" + _seconds;
        }

        public void StopTimer()
        {
            _isPlaying = false;
        }
    }
}
