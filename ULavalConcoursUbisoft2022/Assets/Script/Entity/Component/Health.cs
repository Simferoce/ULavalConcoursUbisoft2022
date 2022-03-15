using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [ContextMenu("Kill")]
    void KillEntity()
    {
        Kill();
    }

    [SerializeField] private float _maxHealth = 0.0f;
    [SerializeField] private float _healthPoint = 0.0f;
    [SerializeField] private Animation _animation = null;
    [SerializeField] private bool _invicible = false;
    [SerializeField] private float _floorHealthLoss = 0.0f;

    [Header("Events")]
    [SerializeField] public UnityEvent<Health> OnDeath;
    [SerializeField] private OnHealthBelowThreasholdEvent[] onHealthBelowThreasholdEvents = null;
    public event Action<Health, float> OnDamage;

    public bool Invicible { get => _invicible; set => _invicible = value; }
    public float MaxHealth { get { return _maxHealth; } }
    public float HealthPoint { get { return _healthPoint; } set { _healthPoint = value; } }
    public float Percentage { get { return HealthPoint / MaxHealth; } }

    [System.Serializable]
    public struct OnHealthBelowThreasholdEvent
    {
        public bool HasBeenInvoke;
        public float PercentageThreashold;
        public UnityEvent OnHealthBelowThreashold;
    }

    public class HealthDamageTakenDTO
    {
        public Health Health { get; set; }
        public float Damage { get; set; }
        public Entity.Team SourceTeam { get; set; }
    }

    public void Awake()
    {
        HealthPoint = MaxHealth;
    }

    public void Hit(float damage, Entity.Team sourceTeam)
    {
        if(!Invicible)
        {
            float damageTaken = _floorHealthLoss > 0.0f ? Mathf.Min(damage, HealthPoint - (HealthPoint % _floorHealthLoss)) : damage;

            HealthPoint -= damageTaken;
            OnDamage?.Invoke(this, damageTaken);
            Channel.Signal("DamageTaken", new HealthDamageTakenDTO() {Health = this, Damage = damageTaken, SourceTeam = sourceTeam });
            
            for(int i =0; i < onHealthBelowThreasholdEvents.Length; ++i)
            {
                if(!onHealthBelowThreasholdEvents[i].HasBeenInvoke && HealthPoint / MaxHealth < onHealthBelowThreasholdEvents[i].PercentageThreashold)
                {
                    onHealthBelowThreasholdEvents[i].OnHealthBelowThreashold?.Invoke();
                    onHealthBelowThreasholdEvents[i].HasBeenInvoke = true;
                }
            }

            if (HealthPoint <= 0)
            {
                Kill();

            }

            if (_animation)
            {
                //Old Animation system only for protype. Do not use that for final. Use the Animator
                _animation.Play();
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
