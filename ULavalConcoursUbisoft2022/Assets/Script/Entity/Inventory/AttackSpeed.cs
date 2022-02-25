using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpeed : MonoBehaviour
{

    [SerializeField] private float attackSpeed = 0f;

    public float getAttackSpeed()
    {
      return attackSpeed;
    }

}