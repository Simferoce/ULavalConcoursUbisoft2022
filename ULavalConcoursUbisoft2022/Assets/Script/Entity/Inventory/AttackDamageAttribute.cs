using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class AttackDamageAttribute
{
    [SerializeField] private float _baseAttackDamage = 1;

    public float GetValue(Inventory inventory)
    {
        return inventory?.Items.Select(x => x.GetComponent<AttackDamage>()).Where(x => x != null)
            .Aggregate(_baseAttackDamage, (x, y) => { return x + y.GetAttackDamage(_baseAttackDamage); }) ?? _baseAttackDamage;
    }
}
