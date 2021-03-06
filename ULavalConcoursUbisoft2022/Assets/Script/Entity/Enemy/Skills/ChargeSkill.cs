using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeSkill : Skill
{
    [Header("Parameters")]
    [SerializeField] private float _cooldown = 0.0f;
    [SerializeField] private GameObject _indicator = null;
    [SerializeField] private GameObject _attack;
    [SerializeField] private float _size = 2.0f;

    [Header("Override Parameters")]
    [SerializeField] private float _maxRange = 0.0f;

    [Header("Reference")]
    [SerializeField] private Entity _entity = null;

    [Header("States")]
    [SerializeField] private PowerUp _powerup = null;
    [SerializeField] private Charge _charge = null;
    [SerializeField] private Incapacited _incapacited = null;

    private float _lastUse = 0.0f;
    private AttackStopOnTrigger _attackInstance = null;
    private GameObject _indicatorInstance = null;

    public PowerUp Powerup { get => _powerup; set => _powerup = value; }
    public Charge Charge { get => _charge; set => _charge = value; }
    public Incapacited Incapacited { get => _incapacited; set => _incapacited = value; }

    private void Awake()
    {
        _charge.MaxRange = _maxRange;
        _incapacited.OnStateDisable += OnIncapicitedStateDisable;
        _charge.OnStateDisable += _charge_OnStateDisable;
        _entity.Health.OnDeath.AddListener(OnDeath);
        Charge.OnStateEnable += Charge_OnStateEnable;
        _lastUse = Time.time - _cooldown;
    }

    private void Charge_OnStateEnable(State obj)
    {
        if (!_entity.Health.IsDead())
        {
            _attackInstance = Instantiate(_attack).GetComponentInChildren<AttackStopOnTrigger>();
            _attackInstance.Team = Entity.Team.Neutral;
            _attackInstance.Following = _entity.transform;
            _attackInstance.Owner = _entity.gameObject;
            BoxCollider collider = _attackInstance.GetComponentInChildren<BoxCollider>();
            collider.enabled = true;
            collider.size = new Vector3(_size * 0.8f, 1, 1);
        }
    }

    private void OnDeath(Health health)
    {
        _entity.Health.OnDeath.RemoveListener(OnDeath);
        if(_attackInstance != null)
        {
            _attackInstance.Destroy();
        }
        Destroy(_indicatorInstance);
    }

    private void _charge_OnStateDisable(State state)
    {
        if (_attackInstance != null)
        {
            _attackInstance.Destroy();
        }
    }

    private void OnDestroy()
    {
        _incapacited.OnStateDisable -= OnIncapicitedStateDisable;
        _charge.OnStateDisable -= _charge_OnStateDisable;
        _powerup.OnStateEnable -= Charge_OnStateEnable;
    }

    private void OnIncapicitedStateDisable(State state)
    {
        InvokeOnSkillFinish();
    }

    public override void Use()
    {
        if(!_entity.Health.IsDead())
        {
            _indicatorInstance = Instantiate(_indicator, this.transform.position, Quaternion.identity);
            Indicator indicator = _indicatorInstance.GetComponentInChildren<Indicator>();
            indicator.Init(_powerup.PowerUpTime, new Vector3(_size, _maxRange), _entity.transform);

            _lastUse = Time.time;
            _powerup.EnableState();
        }
    }

    public override bool CanUse()
    {
        return Time.time - _lastUse > _cooldown;
    }
}
