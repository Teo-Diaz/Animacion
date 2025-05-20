using UnityEngine;

public struct DamagePayload
{
    public enum DamageSeverity
    {
        Light = 1,
        Strong = 2,
        Critical = 3
    }
    
    public float damage;
    public Vector3 position;
    public DamageSeverity severity;
}
