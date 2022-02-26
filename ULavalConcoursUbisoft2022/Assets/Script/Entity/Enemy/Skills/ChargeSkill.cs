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
    private AttackStopOnTrigger _attackInstance;

    public PowerUp Powerup { get => _powerup; set => _powerup = value; }
    public Charge Charge { get => _charge; set => _charge = value; }
    public Incapacited Incapacited { get => _incapacited; set => _incapacited = value; }

    private void Awake()
    {
        _charge.MaxRange = _maxRange;
        _incapacited.OnStateDisable += OnIncapicitedStateDisable;
        _charge.OnStateDisable += _charge_OnStateDisable;
        _lastUse = Time.time - _cooldown;
    }

    private void _charge_OnStateDisable()
    {
        _attackInstance.Destroy();
    }

    private void OnDestroy()
    {
        _incapacited.OnStateDisable -= OnIncapicitedStateDisable;
    }

    private void OnIncapicitedStateDisable()
    {
        InvokeOnSkillFinish();
    }

    public override void Use()
    {
        GameObject gameObject = Instantiate(_indicator, this.transform.position, Quaternion.identity);
        Indicator indicator = gameObject.GetComponentInChildren<Indicator>();
        indicator.Init(_powerup.PowerUpTime, new Vector3(2, _maxRange), _entity.transform);

        _attackInstance = Instantiate(_attack).GetComponentInChildren<AttackStopOnTrigger>();
        _attackInstance.Team = Entity.Team.Neutral;
        _attackInstance.Following = _entity.transform;
        _attackInstance.Owner = _entity.gameObject;

        _lastUse = Time.time;
        _powerup.EnableState();
    }

    public override bool CanUse()
    {
        return Time.time - _lastUse > _cooldown;
    }
}
