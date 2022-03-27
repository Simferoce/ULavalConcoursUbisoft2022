using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class BackToWork : Skill
{
    [Header("Parameters")]
    [SerializeField] private float _duration = 0.0f;
    [SerializeField] private Vector2 _rangeSlowDown = Vector2.zero;

    [Header("Reference")]
    [SerializeField] private NavMeshAgent _navMeshAgent = null;
    [SerializeField] private ProximityStrikeSkill _proximityStrikeSkill = null;
    [SerializeField] private GetCloserToPlayer _getCloseToPlayer = null;
    [SerializeField] private CastSkill _castSkill = null;
    [SerializeField] private State _onWorkDone = null;
    [SerializeField] private State _doWork = null;

    private bool _working = false;
    private float _onWorkStart = 0.0f;

    private float _originalSpeed = 0.0f;
    private float _originalPowerUpTime = 0.0f;
    private float _effectiveness = 1.0f;

    private void Awake()
    {
        _originalSpeed = _navMeshAgent.speed;
        _originalPowerUpTime = _proximityStrikeSkill.PowerUp.PowerUpTime;
    }

    public override bool CanUse()
    {
        return true;
    }

    public override void Use()
    {
        base.Use();
        _proximityStrikeSkill.OnSkillFinish += _proximityStrikeSkill_OnSkillFinish;

        _navMeshAgent.isStopped = false;

        _onWorkStart = Time.time;
        _working = true;

        _doWork.EnableState();
        enabled = true;
    }

    private void _proximityStrikeSkill_OnSkillFinish(Skill obj)
    {
        if (Time.time - _onWorkStart > _duration)
        {
            Finish();
        }
        else
        {
            _doWork.EnableState();
        }
    }

    private void Finish()
    {
        enabled = false;
        _proximityStrikeSkill.OnSkillFinish -= _proximityStrikeSkill_OnSkillFinish;

        _navMeshAgent.speed = _originalSpeed;
        _proximityStrikeSkill.PowerUp.PowerUpTime = _originalPowerUpTime;
        _navMeshAgent.isStopped = true;

        InvokeOnSkillFinish();
        _working = false;
    }

    private void Update()
    {
        if(_working)
        {
            _effectiveness = _rangeSlowDown.x + (1 - (Time.time - _onWorkStart) / _duration) * (_rangeSlowDown.y - _rangeSlowDown.x);
            _navMeshAgent.speed = _originalSpeed * _effectiveness;
            _proximityStrikeSkill.PowerUp.PowerUpTime = _originalPowerUpTime * (1 + (1 - _effectiveness));

            if (Time.time - _onWorkStart > _duration)
            {
                _proximityStrikeSkill.PowerUp.ForceDisableState();
                _proximityStrikeSkill.ProximityStrike.ForceDisableState();
                _getCloseToPlayer.ForceDisableState();
                _castSkill.ForceDisableState();
                Finish();
            }
        }
    }
}
