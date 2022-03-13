using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class DamageReductionAttribute
{
    [SerializeField] private float _baseDamageReduction = 0f;

    public float GetValue(Inventory inventory)
    {
        return inventory?.Items.Select(x => x.GetComponent<DamageReduction>()).Where(x => x != null)
            .Aggregate(_baseDamageReduction, (x, y) => { return x + y.GetDamageReduction(); }) ?? _baseDamageReduction;
    }
}
