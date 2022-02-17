using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityStrikeSkill : Skill
{
    [Header("Parameters")]
    [SerializeField] private float _cooldown;

    [Header("Override Parameters")]
    [SerializeField] private float _range = 0.0f;

    [Header("Reference")]
    [SerializeField] private Entity _entity = null;

    [Header("States")]
    [SerializeField] private PowerUp _powerUp;
    [SerializeField] private ProximityStrike _proximityStrike = null;
    [SerializeField] private GameObject _indicator = null;

    private float _lastTimeUsed = 0.0f;

    private void Awake()
    {
        _proximityStrike.Radius = _range;
        _proximityStrike.OnStateDisable += _proximityStrike_OnStateDisable;
        _lastTimeUsed = -_cooldown - _powerUp.PowerUpTime;
    }

    private void OnDestroy()
    {
        _proximityStrike.OnStateDisable -= _proximityStrike_OnStateDisable;
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
        GameObject gameObject = Instantiate(_indicator, _entity.transform.position, Quaternion.identity);
        Indicator indicator = gameObject.GetComponentInChildren<Indicator>();
        indicator.Init(_powerUp.PowerUpTime, new Vector3(_range * 2, _range * 2), _entity.transform);

        _lastTimeUsed = Time.time;
        _powerUp.EnableState();
    }
}
