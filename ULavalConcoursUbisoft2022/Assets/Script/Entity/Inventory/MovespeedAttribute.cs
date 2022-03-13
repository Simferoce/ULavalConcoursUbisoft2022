using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class MovespeedAttribute
{
    [SerializeField] private float _baseMovespeed = 5;

    public float GetValue(Inventory inventory)
    {
        return inventory?.Items.Select(x => x.GetComponent<Movespeed>()).Where(x => x != null)
            .Aggregate(_baseMovespeed, (x, y) => { return x + y.GetMovespeed(_baseMovespeed); }) ?? _baseMovespeed;
    }
}
