using UnityEngine;

namespace Enemy
{
    [System.Serializable]
    public class Waves
    {
        public string name;
        public Transform enemyTransform;
        public int spawnCount;
        public float spawnRate;
    }
}
