using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform player;

    [Header("Detection")]
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
    private Vector3 walkTarget;
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

        behaviourTree = new Selector(new List<Node>
            {
                attackSequence,
                chaseSequence,
                walkingAction
            });
    }

    private NodeState IsPlayerDetected()
    {
        if (player == null)
        {
            return NodeState.Failure;
        }

        float distance = Vector3.Distance(transform.position, player.position);
        return distance <= detectionRange ? NodeState.Success : NodeState.Failure;
    }

    private NodeState IsPlayerInAttackRange()
    {
        if (player == null)
        {
            return NodeState.Failure;
        }

        float distance = Vector3.Distance(transform.position, player.position);
        return distance <= attackRange ? NodeState.Success : NodeState.Failure;
    }

    private NodeState ChasePlayer()
    {
        if (player == null)
        {
            return NodeState.Failure;
        }

        agent.isStopped = false;
        agent.SetDestination(player.position);

        return NodeState.Running;
    }

    private NodeState AttackPlayer()
    {
        if (player == null)
        {
            return NodeState.Failure;
        }

        agent.isStopped = true;

        Vector3 direction = player.position - transform.position;
        direction.y = 0;

        if (direction.sqrMagnitude > 0.001)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 10 * Time.deltaTime);
        }

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

    private NodeState Walk()
    {
        agent.isStopped = false;

        if (Time.time >= nextWalkTime || (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance))
        {
            Vector3 randomPoint = transform.position + Random.insideUnitSphere * walkRadius;

            NavMeshHit hit;

            if (NavMesh.SamplePosition(randomPoint, out hit, walkRadius, NavMesh.AllAreas))
            {
                walkTarget = hit.position;
                agent.SetDestination(walkTarget);
                nextWalkTime = Time.time + walkInterval;
            }

            nextWalkTime = Time.time + walkInterval;
        }

        return NodeState.Running;
    }

    public void Die()
    {
        isDead = true;
        agent.isStopped = true;
        agent.enabled = false;
    }

    public void SetPlayer(Transform target)
    {
        player = target;
    }
}
