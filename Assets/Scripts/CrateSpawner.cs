using System;
using UnityEngine;
using UnityEngine.AI;

public class CrateSpawner : MonoBehaviour
{
    public GameObject enemyTarget;
    private GameObject _enemy;
    public float repeatTime = 7.0f;
    public float speed = 5.0f;
    public Transform enemyDestination;
        
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(Spawn), 5.0f, repeatTime);
    }

    private void Spawn()
    {
        _enemy = Instantiate(enemyTarget, transform.position, Quaternion.identity);
    }

    private void Update()
    {
        if (_enemy.transform.position == enemyDestination.position) return;
        Vector3 newPos = Vector3.MoveTowards(_enemy.transform.position, enemyDestination.position, speed * Time.deltaTime);
        _enemy.transform.position = newPos;
    }
}