using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityStrikeSkill : Skill
{
    [Header("Parameters")]
    [SerializeField] private float _cooldown;

    [Header("Override Parameters")]
    [SerializeField] private float _range = 0.0f;
    [SerializeField] private Vector3 _offset = Vector3.zero;

    [Header("Reference")]
    [SerializeField] private Entity _entity = null;

    [Header("States")]
    [SerializeField] private PowerUp _powerUp;
    [SerializeField] private ProximityStrike _proximityStrike = null;
    [SerializeField] private GameObject _indicator = null;

    private float _lastTimeUsed = 0.0f;

    public PowerUp PowerUp { get => _powerUp; set => _powerUp = value; }
    public ProximityStrike ProximityStrike { get => _proximityStrike; set => _proximityStrike = value; }

    private AOECircleIndicator _instancedIndicator = null;

    private void Awake()
    {
        _powerUp.OnLock += _powerUp_OnLock;
        _proximityStrike.Radius = _range;
        _proximityStrike.OnStateDisable += _proximityStrike_OnStateDisable;
        _lastTimeUsed = -_cooldown - _powerUp.PowerUpTime;
        _entity.Health.OnDeath += Health_OnDeath;
    }

    private void _powerUp_OnLock()
    {
        _proximityStrike.Target = _entity.transform.position + _entity.transform.rotation * _offset;
    }

    private void Health_OnDeath(Health obj)
    {
        if(_instancedIndicator!= null)
        {
            _instancedIndicator.Destroy();
        }

        _entity.Health.OnDeath -= Health_OnDeath;
    }

    private void OnDestroy()
    {
        _proximityStrike.OnStateDisable -= _proximityStrike_OnStateDisable;
        _powerUp.OnLock -= _powerUp_OnLock;
    }

    private void _proximityStrike_OnStateDisable()
    {
        InvokeOnSkillFinish();
    }

    public override bool CanUse()
    {
        return Time.time - _lastTimeUsed - _powerUp.PowerUpTime > _cooldown;
    }

    public override void Use()
    {
        if (_entity.Health.IsDead())
        {
            return;
        }

        GameObject gameObject = Instantiate(_indicator, _entity.transform.position + _entity.transform.rotation * _offset, Quaternion.identity);
        _instancedIndicator = gameObject.GetComponentInChildren<AOECircleIndicator>();
        _instancedIndicator.Init(_powerUp.PowerUpTime, new Vector3(_range * 2, _range * 2), _entity.transform);

        _lastTimeUsed = Time.time;
        _powerUp.EnableState();
    }
}
