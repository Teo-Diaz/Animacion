using UnityEngine;

public class StateTransition<TContext> where TContext : class
{
    public delegate bool StateTransitonDelegate(TContext context);

    public IStateBehavior<TContext> from;
    public IStateBehavior<TContext> to;

    public StateTransitonDelegate onEvaluate;

    public StateTransition(IStateBehavior<TContext> from, IStateBehavior<TContext> to, StateTransitonDelegate onEvaluate)
    {
        this.from = from;
        this.to = to;
        this.onEvaluate = onEvaluate;
    }
}
