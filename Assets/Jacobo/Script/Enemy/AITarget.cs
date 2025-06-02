using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AITarget : MonoBehaviour
{
    public Transform Target;
    public float DetectionRadius = 10f;
    public float AttackDistance = 2f;
    public float WanderRadius = 15f;
    public float WanderTimer = 5f;
    public LayerMask PlayerLayer;

    private NavMeshAgent agent;
    private Animator animator;
    private float distanceToTarget;
    private float timer;
    private bool isPlayerDetected;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        timer = WanderTimer;
    }


    void Update()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, DetectionRadius, PlayerLayer);
        isPlayerDetected = hits.Length > 0;
        Debug.Log("Detectado!"); 

        if (isPlayerDetected)
        {
            distanceToTarget = Vector3.Distance(transform.position, Target.position);

            if (distanceToTarget <= AttackDistance)
            {
                agent.isStopped = true;
                animator.SetBool("Attack", true);
            }
            else
            {
                agent.isStopped = false;
                agent.SetDestination(Target.position);
                animator.SetBool("Attack", false);
            }
        }
        else
        {
            Wander();
            animator.SetBool("Attack", false);
        }

        animator.SetFloat("Speed", agent.velocity.magnitude);
    }


    void Wander()
    {
        timer += Time.deltaTime;

        if (timer >= WanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, WanderRadius, -1);
            agent.SetDestination(newPos);
            agent.isStopped = false;
            timer = 0;
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * distance;
        randDirection += origin;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, distance, layermask);

        return navHit.position;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, DetectionRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackDistance);
    }
}
