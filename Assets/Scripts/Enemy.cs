using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    // Variables for the NavMeshAgent and for targeting the player
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;

    public int deadEnemy = 1; 
    
    // Patrolling variables
    public Vector3 walkingPoint;
    public float walkingPointRange;
    private bool walkingPointSet;
    
    // Attacking variables
    public float timeBetweenAttacking;
    private bool hasAlreadyAttacked;
    
    // Enemy states and properties
    public float sightRange;
    public float attackRange;
    public bool isPlayerInSightRange;
    public bool isPlayerInAttackRange;
    public float enemyHealth = 0f;
    public float enemySpeed = 0f;
    public float damage = 10f;
    
    // Access to the SpawnEnemy script
    private EnemySpawn enemySpawn;

    private void Awake()
    {
        // Find the player and assign NavMeshAgent and speed
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = enemySpeed; // Set the enemy speed
        
        // Find and access the EnemySpawn script
        enemySpawn = GameObject.Find("EnemySpawn").GetComponent<EnemySpawn>();
    }

    private void Update()
    {
        // Check if the player is in sight or attack range
        isPlayerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        isPlayerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        
        // Decide actions based on player proximity
        if (!isPlayerInSightRange && !isPlayerInAttackRange)
        {
            Patrolling();
        }

        if (isPlayerInSightRange && !isPlayerInAttackRange)
        {
            ChasePlayer();
        }

        if (isPlayerInSightRange && isPlayerInAttackRange)
        {
            AttackingPlayer();
        }
    }

    private void Patrolling()
    {
        if (!walkingPointSet)
        {
            SearchForWalkingPoint();
        }

        if (walkingPointSet)
        {
            agent.SetDestination(walkingPoint);
        }

        Vector3 distanceToWalkingPoint = transform.position - walkingPoint;

        // If the WalkingPoint has been reached, search for a new one
        if (distanceToWalkingPoint.magnitude < 1f)
        {
            walkingPointSet = false;
        }
    }

    private void SearchForWalkingPoint()
    {
        // Generate a random point within the specified range
        float randomX = Random.Range(-walkingPointRange, walkingPointRange);
        float randomZ = Random.Range(-walkingPointRange, walkingPointRange);
        
        // Find a random point
        walkingPoint = new Vector3(transform.position.x + randomX, transform.position.y,
            transform.position.z + randomZ);
        
        // Check if the random point is on the ground using a raycast
        if (Physics.Raycast(walkingPoint,-transform.up, 2f, whatIsGround))
        {
            walkingPointSet = true;
        }
    }
    
    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }
    
    private void AttackingPlayer()
    {
        // Make the enemy look at the player while attacking
        transform.LookAt(player);

        if (!hasAlreadyAttacked)
        {
            Vector3 moveDirection = player.position - transform.position; // Move enemy slightly back towards player
            agent.Move(moveDirection - transform.position);
            hasAlreadyAttacked = true; // Set attack initiated flag
            Invoke(nameof(ResetAttackingPlayer), timeBetweenAttacking); // Reset attack after specified time
        }
    }

    private void ResetAttackingPlayer()
    {
        hasAlreadyAttacked = false;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collided object is the player
        if (other.CompareTag("Player"))
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>(); // Get the PlayerMovement component

            if (player != null)
            {
                player.TakeDamage(damage); // Deal damage to the player
            }
        }
    }
    
    public void TakeDamage(float amount)
    {
        enemyHealth -= amount;
        if (enemyHealth <= 0)
        {
            Die(); // Call the Die method if health drops to or below zero
        }
    }

    private void Die()
    {
        if (ShootingGun.instance != null)
        {
            ShootingGun.instance.IncreaseKillCount(deadEnemy); // Increase kill count if ShootingGun instance exists
        }

        if (enemySpawn != null)
        {
            enemySpawn.RemoveEnemy(gameObject); // Remove enemy using the EnemySpawn script
        }
        
        Destroy(gameObject); // Destroy the enemy object
    }
}
