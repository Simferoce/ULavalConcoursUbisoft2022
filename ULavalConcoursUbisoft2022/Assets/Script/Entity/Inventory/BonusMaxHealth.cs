using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusMaxHealth : MonoBehaviour
{
    [Tooltip("Additional Max Health")]
    [SerializeField] private float bonusMaxHealth = 0f;

    public float GetBonusMaxHealth()
    {
      return bonusMaxHealth;
    }
}
