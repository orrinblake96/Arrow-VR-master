using PillarOfLight;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    public float rotationSpeed = 10f;

    private GameObject _pillar;
    private Transform _target;
    private Transform[] _attackPositions;
    private Transform _pillarTransform;
    private NavMeshAgent _agent;
    private Animator _attackAnimation;

    private PillarHealth _pillarHealth;
    private bool _readyToAttack;
    private static readonly int IsAttacking = Animator.StringToHash("IsAttacking");
    private static readonly int IsCelebrating = Animator.StringToHash("IsCelebrating");

    private void Awake()
    {
        _pillar = GameObject.FindGameObjectWithTag("Pillar");
        _pillarTransform = _pillar.transform;
        _pillarHealth = _pillar.GetComponent<PillarHealth>();
        
        // Pass all attack positions and returns closest 
        _attackPositions = new Transform[3] {GameObject.Find("RedAttackPos").transform, GameObject.Find("GreenAttackPos").transform, GameObject.Find("BlueAttackPos").transform};
        _target = GetClosestAttackPosition(_attackPositions);

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
            MovingTowardsTarget();
            return;
        }
        else
        {
            RotateTowardsTargetWhileAttacking();   
        }
    }
    
    private Transform GetClosestAttackPosition(Transform[] attackPositions)
    {
        Transform closestAttackPosition = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Transform attackPosition in attackPositions)
        {
            float dist = Vector3.Distance(attackPosition.position, currentPos);
            if (dist < minDist)
            {
                closestAttackPosition = attackPosition;
                minDist = dist;
            }
        }
        return closestAttackPosition;
    }

    private void MovingTowardsTarget()
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

    private void RotateTowardsTargetWhileAttacking()
    {
        if (_pillar == null) return;
        Vector3 direction = (_pillarTransform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
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