using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyAI : MonoBehaviour, IStateMachine<BasicEnemyContext>
{
    [field:SerializeField] public BasicEnemyContext context { get; set; }
    public IStateBehavior<BasicEnemyContext> currentState { get ; set ; }
    private IStateBehavior<BasicEnemyContext>[] states;

    private Dictionary<IStateBehavior<BasicEnemyContext>, StateTransition<BasicEnemyContext>[]> transitionsMap;

    public bool EvaluateTransitions()
    {
        foreach (StateTransition<BasicEnemyContext> transition in transitionsMap[currentState])
        {
            if (transition.onEvaluate(context))
            {
                ((IStateMachine<BasicEnemyContext>)this).SwitchState(transition.to);
                return true;
            }
        }

        return false;
    }

    public void UpdateAI()
    {
        currentState.OnUpdate(context);
    }


    private void Awake()
    {
        //Construction code, should be a deserialization stage
        states = new[]
        {
            (IStateBehavior<BasicEnemyContext>)new PatrolState(),
            new ChaseState(),
            new AttackState()
        };

        transitionsMap = new Dictionary<IStateBehavior<BasicEnemyContext>, StateTransition<BasicEnemyContext>[]>();

        transitionsMap.Add(states[0], new[]
        {
            new StateTransition<BasicEnemyContext>(states[0], states[1], (_) => this.context.target != null)
        });
        transitionsMap.Add(states[1], new[]
        {
            new StateTransition<BasicEnemyContext>(states[1], states[2], (_) => this.context.targetDistance < 1)
        });
        transitionsMap.Add(states[2], new[]
        {
            new StateTransition<BasicEnemyContext>(states[2], states[0], (_) => this.context.targetDistance > 10)
        });

        currentState = states[0];
    }

    private void Update()
    {
        EvaluateTransitions();
        //should update in longer intervals, consider implementing space an time partitioning
        UpdateAI();
    }
}
