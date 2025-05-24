using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof(NavMeshAgent))]
[RequireComponent(typeof(CharacterMovement2))]
public class NavMotionController : MonoBehaviour
{
    NavMeshAgent agent;
    CharacterMovement2 characterMovement;

    private void Awake()
    {
        characterMovement = GetComponent<CharacterMovement2>();
        agent = GetComponent<NavMeshAgent>();
        agent.updatePosition = false;
        agent.updateRotation = true;
    }

    private void SolveMotion()
    {
        Vector3 navigationDelta = agent.nextPosition - transform.position;

        float deltaX = Vector3.Dot(transform.right, navigationDelta);
        float deltaY = Vector3.Dot(transform.forward, navigationDelta);

        if(agent.remainingDistance <= agent.stoppingDistance)
        {
            deltaX = 0;
            deltaY = 0;
        }

        characterMovement.SetMotionVector(deltaX * 1.5f, deltaY * 1.5f);
    }

    private void Update()
    {
        SolveMotion();
    }
}
