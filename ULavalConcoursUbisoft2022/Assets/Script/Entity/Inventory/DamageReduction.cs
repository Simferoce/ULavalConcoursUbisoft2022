using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReduction : MonoBehaviour
{
    [Tooltip("Percentage of damage reduction")]
    [SerializeField] private float damageReduction = 0f;

    public float GetDamageReduction()
    {
      return damageReduction;
    }
}
