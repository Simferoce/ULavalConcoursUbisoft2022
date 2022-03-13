using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class AttackSpeedAttribute 
{
    [SerializeField] private float _baseAttackSpeed = 1;

    public float GetValue(Inventory inventory)
    {
        return inventory?.Items.Select(x => x.GetComponent<AttackSpeed>()).Where(x => x != null)
            .Aggregate(_baseAttackSpeed, (x, y) => { return x + y.GetAttackSpeed(_baseAttackSpeed); }) ?? _baseAttackSpeed;
    }
}