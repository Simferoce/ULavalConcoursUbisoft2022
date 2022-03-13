using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movespeed : MonoBehaviour
{
    [Tooltip("Percentage of base movespeed")]
    [SerializeField] private float movespeed = 0f;

    public float GetMovespeed(float baseValue)
    {
      return movespeed * baseValue;
    }
}
