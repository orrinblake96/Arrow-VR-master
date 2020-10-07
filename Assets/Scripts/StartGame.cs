﻿using System;
using UnityEngine;

namespace WaveBasedLevel
{
    public class StartGame : MonoBehaviour
    {
        public GameObject waveManager;
        public GameObject startGameUi;
        public GameObject specialAbilitiesInstructions;
        public GameObject powerupSpawnPoints;
        public GameObject glueBombSpawnPoints;
        public GameObject powerupControllerInstructions;

        private void Update()
        {
            if (gameObject.transform.childCount > 0) return;
            waveManager.SetActive(true);
            startGameUi.SetActive(false);
            gameObject.SetActive(false);
            specialAbilitiesInstructions.SetActive(true);
            powerupSpawnPoints.SetActive(true);
            glueBombSpawnPoints.SetActive(true);
            powerupControllerInstructions.SetActive(true);
        }
    }
}
