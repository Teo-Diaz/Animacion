using UnityEngine;
using UnityEngine.AI;

public class ChaseState : IStateBehavior<BasicEnemyContext>
{
    public void OnEnter(BasicEnemyContext context)
    {

    }

    public void OnUpdate(BasicEnemyContext context)
    {
        if (context.target == null)
        {
            context.targetDistance = Vector3.Distance(context.agent.transform.position, context.player.transform.position);
            return; 
        }

        NavMeshAgent navigationAgent = context.agent.GetComponent<NavMeshAgent>();
        navigationAgent.destination = context.target.position;
        context.targetDistance = navigationAgent.remainingDistance;
    }
}
