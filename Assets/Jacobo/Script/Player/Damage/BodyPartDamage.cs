using UnityEngine;

public class BodyPartDamage : MonoBehaviour
{
    [SerializeField] private DamageController rootReceiver;

    public void ReceiveDamage(IDamageSender perpetrator, DamagePayload payload)
    {
        rootReceiver.ReceiveDamage(perpetrator, payload);
    }

    public int Faction => rootReceiver.Faction;
}
