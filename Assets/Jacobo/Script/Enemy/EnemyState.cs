using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class EnemyState : MonoBehaviour
{
    public enum State { Patrol, Chase, Attack }

    [Header("General Settings")]
    public float detectionRadius = 10f;
    public float attackDistance = 2f;
    public LayerMask playerLayer;
    public float patrolRadius = 5f;

    [Header("Combat Settings")]
    public GameObject swordHitbox; 

    private NavMeshAgent agent;
    private Animator animator;
    private Transform player;

    private State currentState = State.Patrol;
    private Vector3 patrolTarget;
    private bool hasPatrolTarget = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        if (swordHitbox != null)
            swordHitbox.SetActive(false);
    }

    private void Update()
    {
        DetectPlayer();

        switch (currentState)
        {
            case State.Patrol:
                Patrol();
                break;
            case State.Chase:
                Chase();
                break;
            case State.Attack:
                Attack();
                break;
        }

        UpdateAnimatorSpeed();
    }

    private void DetectPlayer()
    {
        if (player == null)
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius, playerLayer);
            if (hits.Length > 0)
            {
                player = hits[0].transform;
                currentState = State.Chase;
            }
        }
        else
        {
            float distance = Vector3.Distance(transform.position, player.position);

            if (distance > detectionRadius)
            {
                player = null;
                currentState = State.Patrol;
                agent.isStopped = false;
                animator.SetBool("Attack", false); // Apagar animación de ataque
            }
            else if (distance <= attackDistance)
            {
                currentState = State.Attack;
            }
            else
            {
                currentState = State.Chase;
            }
        }
    }

    private void Patrol()
    {
        animator.SetBool("Attack", false);

        if (!hasPatrolTarget || Vector3.Distance(transform.position, patrolTarget) < 1f)
        {
            Vector2 randomPos = Random.insideUnitCircle * patrolRadius;
            patrolTarget = transform.position + new Vector3(randomPos.x, 0, randomPos.y);
            hasPatrolTarget = true;
            agent.isStopped = false;
            agent.SetDestination(patrolTarget);
        }
    }

    private void Chase()
    {
        if (player != null)
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
            animator.SetBool("Attack", false); // Asegura que se detenga el ataque si venía atacando
        }
    }

    private void Attack()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > attackDistance)
        {
            currentState = State.Chase;
            agent.isStopped = false;
            animator.SetBool("Attack", false);
            return;
        }

        agent.isStopped = true;

        Vector3 direction = player.position - transform.position;
        direction.y = 0;
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }
        animator.SetBool("Attack", true);
    }

    private void UpdateAnimatorSpeed()
    {
        float speed = agent.isStopped ? 0f : agent.velocity.magnitude;
        animator.SetFloat("Speed", speed);
    }

    public void EnableHitbox()
    {
        if (swordHitbox != null)
            swordHitbox.SetActive(true);
    }

    public void DisableHitbox()
    {
        if (swordHitbox != null)
            swordHitbox.SetActive(false);
    }   

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }
#endif
}