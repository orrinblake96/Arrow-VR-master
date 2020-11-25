using UnityEngine;

namespace PillarOfLight
{
    public class EndlessDifficultyStats : MonoBehaviour
    {
        public float DifficultySpeedBoost { get; set; } = 0f;
        public int DifficultyEnemySpawnsIncrease { get; set; } = 0;
        public float SpawnChance { get; set; } = 0f;
        public float LargeEnemySpawnChance { get; set; } = 0f;
    }
}
