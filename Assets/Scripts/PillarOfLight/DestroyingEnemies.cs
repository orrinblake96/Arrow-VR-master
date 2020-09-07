using System;
using UnityEngine;
using WaveBasedLevel;

namespace PillarOfLight
{
    public class DestroyingEnemies : MonoBehaviour
    {
        public GameObject explosionParticles;
        private WaveScore _waveScoreBoard;
        private SpecialAbilitiesBar _specialAbilitiesBar;
        private bool _scoreBoardExists = false;

        private void Awake()
        {
            if (GameObject.FindGameObjectWithTag("WaveScoreBoard") != null)
            {
                _waveScoreBoard = GameObject.FindGameObjectWithTag("WaveScoreBoard").GetComponent<WaveScore>();
                _specialAbilitiesBar = GameObject.Find("AbilitiesSlider").GetComponent<SpecialAbilitiesBar>();
                _scoreBoardExists = true;
            }
        }

        private void OnDestroy()
        {
            Instantiate(explosionParticles, transform.position + Vector3.up, transform.rotation);
            
            if (_scoreBoardExists)
            {
                _waveScoreBoard.IncreaseCurrentScore();
                _specialAbilitiesBar.IncrementPower(.05f);
            }
        }
    }
}
