using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform player;

    [SerializeField] private float detectionRange = 10;

    [Header("Combat")]
    [SerializeField] private float attackRange = 2;
    [SerializeField] private int attackDamage = 10;
    [SerializeField] private float attackCooldown = 1;

    [Header("Movement")]
    [SerializeField] private float walkRadius = 8;
    [SerializeField] private float walkInterval = 4;

    private NavMeshAgent agent;
    private Node behaviourTree;

    private float nextAttackTime;
    private float nextWalkTime;
    private bool isDead;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        BuildBehaviourTree();
    }

    private void Update()
    {
        if (isDead)
        {
            return;
        }

        behaviourTree.Tick();
    }

    // Setting up the behavior tree structure
    private void BuildBehaviourTree()
    {
        Node attackSequence = new Sequence(new List<Node>
            {
                new ActionNode(IsPlayerInAttackRange),
                new ActionNode(AttackPlayer)
            });

        Node chaseSequence = new Sequence(new List<Node>
            {
                new ActionNode(IsPlayerDetected),
                new ActionNode(ChasePlayer)
            });

        Node walkingAction = new ActionNode(Walk);

        // Attack first, then chase, and then walk if the player is not detected
        behaviourTree = new Selector(new List<Node>
            {
                attackSequence,
                chaseSequence,
                walkingAction
            });
    }

    // Check if the player is close enoough to chase
    private NodeState IsPlayerDetected()
    {
        if (player == null)
        {
            return NodeState.Failure;
        }

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectionRange)
        {
            return NodeState.Success;
        }

        return NodeState.Failure;
    }

    // Check if the player is close enough to attack
    private NodeState IsPlayerInAttackRange()
    {
        if (player == null)
        {
            return NodeState.Failure;
        }

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attackRange)
        {
            return NodeState.Success;
        }

        return NodeState.Failure;
    }

    // Move towards the player
    private NodeState ChasePlayer()
    {
        agent.isStopped = false;
        agent.SetDestination(player.position);

        FacePlayer();

        return NodeState.Running;
    }

    // Attack the player
    private NodeState AttackPlayer()
    {
        if (player == null)
        {
            return NodeState.Failure;
        }
        
        agent.isStopped = true;

        FacePlayer();

        // Attack when the cooldown has finished
        if (Time.time >= nextAttackTime)
        {
            nextAttackTime = Time.time + attackCooldown;

            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
            }
        }

        return NodeState.Running;
    }

    // Walk to random points when the player is not detected
    private NodeState Walk()
    {
        agent.isStopped = false;

        // Wait until its time to choose another direction
        if (Time.time < nextWalkTime && (agent.pathPending || agent.remainingDistance > agent.stoppingDistance))
        {
            return NodeState.Running;
        }

        Vector3 randomPoint = transform.position + Random.insideUnitSphere * walkRadius;

        NavMeshHit hit;

        // Only walk to a point on the NavMesh
        if (NavMesh.SamplePosition(randomPoint, out hit, walkRadius, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }

        nextWalkTime = Time.time + walkInterval;

        return NodeState.Running;
    }

    // Stop the zombie from moving and attacking when it dies
    public void Die()
    {
        isDead = true;
        agent.isStopped = true;
        agent.enabled = false;
    }

    // Set the player target for the zombie
    public void SetPlayer(Transform target)
    {
        player = target;
    }

    private void FacePlayer()
    {
        // Turn to face the player
        Vector3 direction = player.position - transform.position;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10 * Time.deltaTime);
        }
    }
}
