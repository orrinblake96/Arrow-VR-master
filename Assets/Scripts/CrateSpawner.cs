using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CrateSpawner : MonoBehaviour
{
    public float spawnDelay = 2.0f;
    public float spawnTime = 8.0f;
    public GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
//        InvokeRepeating(nameof(Spawn), spawnDelay, spawnTime);
    }

    private void Spawn()
    {
//        Instantiate(enemy, transform.position, transform.rotation);
    }

    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.Three) || Input.GetKeyDown(KeyCode.D))
        {
            Instantiate(enemy, transform.position, transform.rotation);
        }
    }
}