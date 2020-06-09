using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    private Transform _target;
    private NavMeshAgent _agent;
    private Animator _attackAnimation;
    private bool _readyToAttack;
    private static readonly int IsAttacking = Animator.StringToHash("IsAttacking");

    private void Awake()
    {
        switch (gameObject.name)
        {
            case "BasicPaintMonsterRed(Clone)":
                _target = GameObject.Find("RedAttackPos").transform;
                break;
            case "BasicPaintMonsterGreen(Clone)":
                _target = GameObject.Find("GreenAttackPos").transform;
                break;
            default:
                _target = GameObject.Find("BlueAttackPos").transform;
                break;
        }
    }

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _attackAnimation = GetComponent<Animator>();
        
        _agent.SetDestination(_target.position);
    }

    private void Update()
    {
        if (!_readyToAttack)
        {
            if (!_agent.pathPending)
            {
                if (_agent.remainingDistance <= _agent.stoppingDistance)
                {
                    if (!_agent.hasPath || _agent.velocity.sqrMagnitude == 0f)
                    {
                        _readyToAttack = true;
                        _attackAnimation.SetBool(IsAttacking, true);
                        Debug.Log("Arrived");
                    }
                }
            }
        }
    }
}