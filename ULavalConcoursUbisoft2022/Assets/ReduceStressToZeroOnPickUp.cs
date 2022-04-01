using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReduceStressToZeroOnPickUp : ItemAction
{
    public override void Execute()
    {
        Health health = GameObject.FindObjectOfType<Player>().GetComponentInChildren<Health>();
        health.HealthPoint = health.MaxHealth;
    }
}
