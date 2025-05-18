using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    private List<DamageMessage> damageList = new List<DamageMessage>();

    public void EnqueueDamage(DamageMessage damage)
    {
        if(damageList.Any(dmg => dmg.sender == damage.sender)) return;
        damageList.Add(damage);
    }

    private void Update()
    {
        foreach(DamageMessage message in damageList)
        {
            Game.Instance.PlayerOne.DepleteHealth(message.amount);
        }
    }
}
