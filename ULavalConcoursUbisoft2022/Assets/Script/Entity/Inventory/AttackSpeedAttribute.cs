using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpeedAttribute 
{
    [SerializeField] private float _baseAttackSpeed = 1;

    private AttackSpeed[] attackSpeedItems = null;
    private float effectiveAttackSpeed = 0f;

    public AttackSpeedAttribute(float baseAttackSpeed)
    {
        _baseAttackSpeed = baseAttackSpeed;
    }

    public float getValue(Inventory inventory)
    {
        attackSpeedItems = inventory.GetComponentsInChildren<AttackSpeed>();
        Debug.Log(attackSpeedItems.Length);

        effectiveAttackSpeed = _baseAttackSpeed;
        foreach (AttackSpeed item in attackSpeedItems)
        {
            effectiveAttackSpeed += _baseAttackSpeed * item.getAttackSpeed();
        }

        Debug.Log(effectiveAttackSpeed);
        return effectiveAttackSpeed;
    }

}