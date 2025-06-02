using UnityEngine;

public struct DamagePayload
{
    public enum DamageSeverity
    {
        Light = 1,
        Critical = 2,
    }
    
    public float damageAmount;
    public Vector3 position;
    public DamageSeverity severity;
}
