using System;
using PillarOfLight;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    private Transform _target;
    private NavMeshAgent _agent;
    private Animator _attackAnimation;

    private PillarHealth _pillarHealth;
    private bool _readyToAttack;
    private static readonly int IsAttacking = Animator.StringToHash("IsAttacking");
    private static readonly int IsCelebrating = Animator.StringToHash("IsCelebrating");

    private void Awake()
    {
        // Reference pillar ************ Refine Later ******************
        _pillarHealth = GameObject.FindGameObjectWithTag("Pillar").GetComponent<PillarHealth>();
        
        // *********************** Refinable in future ***********************
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
        // When enemies reach navmesh location they should attack
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
    
    // Called as an animation event on swinging animation
    public void AttackPillar()
    {
        // Attack Pillar while it has health
        if (_pillarHealth.currentHealth > 0)
        {
            _pillarHealth.DamageTaken();
        }
        else
        {
            // Celebrate a glorious victory
            _attackAnimation.SetBool(IsCelebrating, true);
        }
    }
}