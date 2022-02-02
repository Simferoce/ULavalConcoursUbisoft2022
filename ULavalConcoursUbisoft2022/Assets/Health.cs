using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 0.0f;
    public float MaxHealth { get { return _maxHealth; } }

    [SerializeField] private float _healthPoint = 0.0f;
    public float HealthPoint { get { return _healthPoint; } private set { _healthPoint = value; } }

    public void Awake()
    {
        HealthPoint = MaxHealth;
    }

    public void Hit(float damage)
    {
        HealthPoint -= damage;
    }
}
