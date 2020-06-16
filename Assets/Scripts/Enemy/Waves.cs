using UnityEngine;

namespace Enemy
{
    [System.Serializable]
    public class Waves
    {
        public string name;
        public Transform[] enemyTransforms;
        public int spawnCount;
        public float spawnRate;
    }
}
