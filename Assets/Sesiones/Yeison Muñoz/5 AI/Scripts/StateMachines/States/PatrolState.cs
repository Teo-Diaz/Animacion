using UnityEngine;
using UnityEngine.AI;

public class PatrolState : IStateBehavior<BasicEnemyContext>
{
    [SerializeField] private float searchRadius = 10f;
    [SerializeField] private Vector2 searchTime = new Vector2(3,5);

    private float timer;
    private float currentTime;

    public void OnEnter(BasicEnemyContext context)
    {
        Debug.Log("Patrol c:");
    }

    public void OnUpdate(BasicEnemyContext context)
    {
        NavMeshAgent navigationAgent = context.agent.GetComponent<NavMeshAgent>();
        if (timer > currentTime)
        {
            Vector3 targetPosition = Vector3.ProjectOnPlane(Random.insideUnitSphere * Random.Range(0, searchRadius), context.agent.transform.up);
            navigationAgent.destination = targetPosition;
            timer = 0;
            currentTime = Random.Range(searchTime.x, searchTime.y);
            Debug.Log($"wtf {targetPosition}");
        }

        timer += Time.deltaTime;
        Debug.Log($"wtf {timer}");

        context.targetDistance = Vector3.Distance(navigationAgent.transform.position, context.player.transform.position);
    }
}
