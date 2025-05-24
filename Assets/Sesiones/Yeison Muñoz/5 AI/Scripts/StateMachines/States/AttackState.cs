using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.Android;

public class AttackState : IStateBehavior<BasicEnemyContext>
{
    [SerializeField] private Vector2 waitTime = new Vector2(2f, 3f);
    [SerializeField] private string attackTriggerName = "Attack";

    private int attackTriggerId;

    private float timer;
    private float currentTime;

    private Animator animator;

    public void OnEnter(BasicEnemyContext context)
    {
        attackTriggerId = Animator.StringToHash(attackTriggerName);
        animator = context.agent.GetComponent<Animator>(); 
    }

    public void OnUpdate(BasicEnemyContext context)
    {
        if(timer > currentTime)
        {
            animator.SetTrigger(attackTriggerId);
            timer = 0;
            currentTime = Random.Range(waitTime.x, waitTime.y);
        }
        timer += Time.deltaTime;

        context.targetDistance = Vector3.Distance(context.agent.transform.position, context.player.transform.position);
    }
}
