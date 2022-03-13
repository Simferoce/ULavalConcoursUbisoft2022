using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDamage : MonoBehaviour
{
    [Tooltip("Percentage of base attack damage")]
    [SerializeField] private float attackDamage = 0f;

    public float GetAttackDamage(float baseValue)
    {
      return attackDamage * baseValue;
    }
}
