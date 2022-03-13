using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeSkill : Skill
{
    [Header("Parameters")]
    [SerializeField] private float _cooldown = 0.0f;
    [SerializeField] private GameObject _indicator = null;
    [SerializeField] private GameObject _attack;

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
        _lastUse = Time.time - _cooldown;
    }

    private void OnDeath(Health health)
    {
        _entity.Health.OnDeath.RemoveListener(OnDeath);
        _attackInstance.Destroy();
        Destroy(_indicatorInstance);
    }

    private void _charge_OnStateDisable(State state)
    {
        _attackInstance.Destroy();
    }

    private void OnDestroy()
    {
        _incapacited.OnStateDisable -= OnIncapicitedStateDisable;
        _charge.OnStateDisable -= _charge_OnStateDisable;
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
            indicator.Init(_powerup.PowerUpTime, new Vector3(2, _maxRange), _entity.transform);

            _attackInstance = Instantiate(_attack).GetComponentInChildren<AttackStopOnTrigger>();
            _attackInstance.Team = Entity.Team.Neutral;
            _attackInstance.Following = _entity.transform;
            _attackInstance.Owner = _entity.gameObject;
            _attackInstance.GetComponentInChildren<BoxCollider>().enabled = true;

            _lastUse = Time.time;
            _powerup.EnableState();
        }
    }

    public override bool CanUse()
    {
        return Time.time - _lastUse > _cooldown;
    }
}
