using UnityEngine;

[DefaultExecutionOrder(-1)]
public class Character : MonoBehaviour
{
    [SerializeField] Transform lockTarget;

    
    public Transform LockTarget
    {
        get => lockTarget;
        set => lockTarget = value;
    }

    private void RegisterComponents()
    {
        foreach (ICharacterComponent characterComponent in GetComponentsInChildren<ICharacterComponent>())
        {
            characterComponent.ParentCharacter = this;
        }
    }

    private void Awake()
    {
        RegisterComponents();
    }
}
