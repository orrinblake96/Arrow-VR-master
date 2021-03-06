﻿using System.Collections;
using PillarOfLight;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class EnemyBehaviour : MonoBehaviour
    {
        public float rotationSpeed = 10f;
        public GameObject fracturedSelf;
        
        [Header("Enemy Size")]
        public bool isLargeEnemy;
        
        [Header("Glue Power-up")]
        public float glueWaitTime = 7f;
        public GameObject glue;

        private GameObject _pillar;
        private Transform _target;
        private Transform[] _attackPositions;
        private Transform _pillarTransform;
        private NavMeshAgent _agent;
        private Animator _attackAnimation;
        private WaveSpawner _waveSpawnedInformation;
        private DestroyingEnemies _destroyingEnemies;
        private DestroyLargeEnemy _destroyLargeEnemy;
        private EndlessDifficultyStats _endlessDifficultyStats;

        private PillarHealth _pillarHealth;
        private bool _readyToAttack;
        private static readonly int IsWalking = Animator.StringToHash("IsWalking");
        private static readonly int IsAttacking = Animator.StringToHash("IsAttacking");
        private static readonly int IsCelebrating = Animator.StringToHash("IsCelebrating");
        private static readonly int IsStuck = Animator.StringToHash("IsStuck");

        private void Awake()
        {
            _pillar = GameObject.Find("PillarOfLightTarget");
            _pillarTransform = _pillar.transform;
            _pillarHealth = _pillar.GetComponent<PillarHealth>();
            // Pass all attack positions and returns closest 
            if (!isLargeEnemy)
            {
                _attackPositions = new Transform[] {GameObject.Find("RedAttackPos").transform, GameObject.Find("GreenAttackPos").transform, GameObject.Find("BlueAttackPos").transform};
                _target = GetClosestAttackPosition(_attackPositions);
                _destroyingEnemies = GetComponent<DestroyingEnemies>();
            }
            else
            {
                _attackPositions = new Transform[] {GameObject.Find("LargeRedAttackPos").transform, GameObject.Find("LargeGreenAttackPos").transform, GameObject.Find("LargeBlueAttackPos").transform};
                _target = GetClosestAttackPosition(_attackPositions);
                _destroyLargeEnemy = GetComponent<DestroyLargeEnemy>();
            }
        }

        private void Start()
        {
            _waveSpawnedInformation = GameObject.Find("GM").GetComponent<WaveSpawner>();

            _agent = GetComponent<NavMeshAgent>();
            _attackAnimation = GetComponent<Animator>();
        
            // Set destination to target and calculate speed of gent to increase as they beat more rounds
            _endlessDifficultyStats = GameObject.Find("DifficultyManager").GetComponent<EndlessDifficultyStats>();
            _agent.SetDestination(_target.position);
            _agent.speed += (_waveSpawnedInformation.currentRoundNumber * .2f) + _endlessDifficultyStats.DifficultySpeedBoost;
        }

        private void Update()
        {
            if (!_pillar.activeSelf)
            {
                _attackAnimation.SetBool(IsCelebrating, true);
                StartCoroutine(SelfDestroy());
            }
            else
            {
                // When enemies reach navmesh location they should attack
                if (!_readyToAttack)
                {
                    MovingTowardsTarget();
                    return;
                }

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

        private IEnumerator SelfDestroy()
        {
            yield return  new WaitForSeconds(1.5f);
            // Add Damage code
            if (isLargeEnemy) _destroyLargeEnemy.Damage(100);
            if (!isLargeEnemy) _destroyingEnemies.Damage(10);

        } 
    
        // Called as an animation event on swinging animation
        public void AttackPillar()
        {
            // Attack Pillar while it has health
            if (_pillarHealth.CurrentHealth > 0)
            {
                _pillarHealth.DamageTaken();
            }
        }
        
        // Stop enemies moving for set time, special ability
        public void GlueBomb()
        {
            StartCoroutine(GlueEnemies());
        }

        private IEnumerator GlueEnemies()
        {
            _attackAnimation.SetBool(IsWalking, false);
            _attackAnimation.SetBool(IsStuck, true);
            
            glue.SetActive(true);
            _agent.isStopped = true;
            _agent.velocity = Vector3.zero;

            yield return new WaitForSeconds(glueWaitTime);
            
            glue.SetActive(false);
            _attackAnimation.SetBool(IsStuck, false);
            _attackAnimation.SetBool(IsWalking, true);
            _agent.isStopped = false;
        }
    }
}