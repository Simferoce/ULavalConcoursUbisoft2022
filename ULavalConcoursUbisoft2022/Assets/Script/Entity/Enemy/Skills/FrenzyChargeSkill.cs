using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrenzyChargeSkill : Skill
{
    [Header("Parameters")]
    [SerializeField] private int _numberOfCharge = 0;
    [SerializeField] private float _acceleration = 0.0f;
    [SerializeField] private float _lastChargeStunDuration = 0.0f;

    [Header("Reference")]
    [SerializeField] private Entity _entity = null;

    [Header("States")]
    [SerializeField] private ChargeSkill _charge = null;

    private float _originAccelaration = 0.0f;
    private float _originSpeed = 0.0f;
    private float _originIncapacitedTime = 0.0f;
    private float _originPowerUpTime = 0.0f;
    private int _numberOfChargeDone = 0;

    private void Awake()
    {
        _charge.Charge.OnStateDisable += Charge_OnStateDisable;
        _charge.OnSkillFinish += _charge_OnSkillFinish;
    }

    private void Charge_OnStateDisable()
    {
       if (_numberOfChargeDone + 1 == _numberOfCharge)
        {
            _entity.Health.Invicible = false;
        }
    }

    private void OnDestroy()
    {
        _charge.OnSkillFinish -= _charge_OnSkillFinish;
        _charge.OnSkillFinish -= _charge_OnSkillFinish;
    }

    private void _charge_OnSkillFinish(Skill skill)
    {
        _numberOfChargeDone++;

        if(_numberOfChargeDone < _numberOfCharge)
        {
            Charge();
        }
        else
        {
            _charge.Charge.Acceleration = _originAccelaration;
            _charge.Charge.Speed = _originAccelaration;
            _charge.Incapacited.Time = _originIncapacitedTime;
            _charge.Powerup.PowerUpTime = _originPowerUpTime;

            _numberOfChargeDone = 0;
            InvokeOnSkillFinish();
        }
    }

    public override bool CanUse()
    {
        return true;
    }

    public override void Use()
    {
        _entity.Health.Invicible = true;
        _originAccelaration = _charge.Charge.Acceleration;
        _originSpeed = _charge.Charge.Speed;
        _originIncapacitedTime = _charge.Incapacited.Time;
        _originPowerUpTime = _charge.Powerup.PowerUpTime;

        if (_numberOfChargeDone < _numberOfCharge)
        {
            Charge();
        }
    }

    private void Charge()
    {
        _charge.Charge.Acceleration *= _acceleration;
        _charge.Charge.Speed *= _acceleration;
        _charge.Powerup.PowerUpTime /= _acceleration;
        _charge.Incapacited.Time = _numberOfChargeDone + 1 == _numberOfCharge ? _lastChargeStunDuration : 0.0f;

        _charge.Use();
    }
}
