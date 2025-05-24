using UnityEngine;

public interface IStateBehavior<TContext> where TContext : class
{
    void OnEnter(TContext context);
    void OnUpdate(TContext context);
}
