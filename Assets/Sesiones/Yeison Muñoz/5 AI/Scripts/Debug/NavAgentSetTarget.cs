using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavAgentSetTarget : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] private Transform target;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        agent.SetDestination(target.position);
    }
}
