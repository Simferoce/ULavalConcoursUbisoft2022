using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpeed : MonoBehaviour
{
    [Tooltip("Percentage of base attack speed")]
    [SerializeField] private float attackSpeed = 0f;

    public float GetAttackSpeed(float baseValue)
    {
      return attackSpeed * baseValue;
    }

}