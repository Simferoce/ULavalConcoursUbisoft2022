using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 0.0f;
    public float MaxHealth { get { return _maxHealth; } }

    [SerializeField] private float _healthPoint = 0.0f;
    public float HealthPoint { get { return _healthPoint; } set { _healthPoint = value; } }

    [SerializeField] private Animation _animation = null;

    [SerializeField] private bool _invicible = false;

    public bool Invicible { get => _invicible; set => _invicible = value; }

    public UnityEvent<Health> OnDeath;
    public event Action<Health, float> OnDamage;

    public void Awake()
    {
        HealthPoint = MaxHealth;
    }

    public void Hit(float damage)
    {
        if(!Invicible)
        {
            HealthPoint -= damage;
            OnDamage?.Invoke(this, damage);
            
            if (_animation)
            {
                //Old Animation system only for protype. Do not use that for final. Use the Animator
                _animation.Play();
            }

            if (HealthPoint <= 0)
            {
                Kill();
                
            }
        }
    }

    public void Kill()
    {
        HealthPoint = 0;
        OnDeath?.Invoke(this);
        Destroy(transform.parent.gameObject, 0.1f);
        _animation.Play("tempAnimOnDeath");
    }

    public bool IsDead()
    {
        return _healthPoint <= 0;
    }
}
