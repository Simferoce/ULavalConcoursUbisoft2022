using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class BonusMaxHealthAttribute 
{

    [SerializeField] private float _baseBonusHealth = 0;

    public float GetValue(Inventory inventory)
    {
        return inventory?.Items.Select(x => x.GetComponent<BonusMaxHealth>()).Where(x => x != null)
            .Aggregate(_baseBonusHealth, (x, y) => { return x + y.GetBonusMaxHealth(); }) ?? _baseBonusHealth;
    }
}
