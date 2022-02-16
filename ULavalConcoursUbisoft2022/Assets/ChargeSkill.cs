using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeSkill : Skill
{
    [Header("Parameters")]
    [SerializeField] private float _cooldown = 0.0f;
    [SerializeField] private GameObject _indicator = null;

    [Header("Override Parameters")]
    [SerializeField] private float _maxRange = 0.0f;

    [Header("Reference")]
    [SerializeField] private Entity _entity = null;

    [Header("States")]
    [SerializeField] private PowerUp _powerup = null;
    [SerializeField] private Charge _charge = null;
    [SerializeField] private Incapacited _incapacited = null;

    private float _lastUse = 0.0f;

    private void Awake()
    {
        _charge.MaxRange = _maxRange;
        _incapacited.OnStateDisable += OnIncapicitedStateDisable;
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

        _lastUse = Time.time;
        _powerup.EnableState();
    }

    public override bool CanUse()
    {
        return Time.time - _lastUse > _cooldown;
    }
}
