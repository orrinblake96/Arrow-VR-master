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
            DifficultySelected();
            HandleObjects();
            
            Instantiate(explosionParticles, transform.position + Vector3.up, Quaternion.Euler(-90f, 0f, 0f));
            Destroy(gameObject);
        }

        private void DifficultySelected()
        {
            string difficulty = gameObject.name;

            if (difficulty.Contains("Easy"))
            {
                _endlessDifficultyStats.DifficultySpeedBoost = 0f;
                _endlessDifficultyStats.DifficultyEnemySpawnsIncrease = 0;
                _endlessDifficultyStats.SpawnChance = 0f;
                _endlessDifficultyStats.LargeEnemySpawnChance = 0.3f;
            }
            if (difficulty.Contains("Medium"))
            {
                _endlessDifficultyStats.DifficultySpeedBoost = 0.5f;
                _endlessDifficultyStats.DifficultyEnemySpawnsIncrease = 2;
                _endlessDifficultyStats.SpawnChance = 0.3f;
                _endlessDifficultyStats.LargeEnemySpawnChance = 0.5f;
            }
            if (difficulty.Contains("Hard"))
            {
                _endlessDifficultyStats.DifficultySpeedBoost = 1.0f;
                _endlessDifficultyStats.DifficultyEnemySpawnsIncrease = 4;
                _endlessDifficultyStats.SpawnChance = 0.5f;
                _endlessDifficultyStats.LargeEnemySpawnChance = 0.7f;
            }
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
