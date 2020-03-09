using UnityEngine;

namespace _BowAndArrow.Scripts
{
    public class CrateSpawner : MonoBehaviour
    {
        public GameObject prefab;
        public float repeatTime = 3.0f;
        
        // Start is called before the first frame update
        void Start()
        {
            InvokeRepeating("Spawn", 5.0f, repeatTime);
//            nextSpawnTime = Time.time + 5.0f;
        }

        void Spawn()
        {
            Instantiate(prefab, transform.position, Quaternion.identity);
        }
    }
}
