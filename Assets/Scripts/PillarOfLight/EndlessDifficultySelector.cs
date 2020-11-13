using System;
using Crate;
using UnityEngine;

namespace PillarOfLight
{
    public class EndlessDifficultySelector : MonoBehaviour,IDamageable
    {
        [SerializeField] private  GameObject explosionParticles;
        
        [Header("Object To Remove")]
        [SerializeField] private GameObject[] removeableObjects;

        [Header("Objects To Show")] 
        [SerializeField] private GameObject[] showableObjects;

        private EndlessDifficultyStats _endlessDifficultyStats;

        private void Start()
        {
            _endlessDifficultyStats = GameObject.Find("DifficultyManager").GetComponent<EndlessDifficultyStats>();
        }

        public void Damage(int amount)
        {
            _endlessDifficultyStats.DifficultySpeedBoost = DifficultySelected();
            HandleObjects();
            
            Instantiate(explosionParticles, transform.position + Vector3.up, Quaternion.Euler(-90f, 0f, 0f));
            Destroy(gameObject);
        }

        private float DifficultySelected()
        {
            string difficulty = gameObject.name;

            if (difficulty.Contains("Easy"))
            {
                _endlessDifficultyStats.DifficultyEnemySpawnsIncrease = 0;
                return 0.2f;
            }
            if (difficulty.Contains("Medium"))
            {
                _endlessDifficultyStats.DifficultyEnemySpawnsIncrease = 2;
                return 1.0f;
            }
            if (difficulty.Contains("Hard"))
            {
                _endlessDifficultyStats.DifficultyEnemySpawnsIncrease = 4;
                return 1.5f;
            }

            return 0f;
        }

        private void HandleObjects()
        {
            foreach (GameObject objectToRemove in removeableObjects)
            {
                Destroy(objectToRemove);
            }
            
            foreach (GameObject objectToShow in showableObjects)
            {
                objectToShow.SetActive(true);
            }
        }
    }
}
