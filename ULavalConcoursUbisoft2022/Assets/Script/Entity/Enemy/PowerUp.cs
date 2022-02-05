using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PowerUp : State
{
    [Header("Parameters")]
    [SerializeField] private float _powerUpTime = 0.0f;
    [SerializeField] private float _lockPercentage = 0.0f;

    [Header("Reference")]
    [SerializeField] private Entity _entity = null;
    [SerializeField] private NavMeshAgent _navMeshAgent = null;

    [Header("State")]
    [SerializeField] private State _releaseState = null;

    private Player _player = null;
    private bool _locked = false;
    private float _powerStartTime = 0.0f;

    protected override void Init()
    {
        _player = FindObjectOfType<Player>();
    }

    protected override void OnEnter()
    {
        _navMeshAgent.isStopped = true;
        _powerStartTime = Time.time;
    }

    protected override void OnExit()
    {
        _locked = false;
    }

    protected override void OnUpdate()
    {
        if(Time.time -_powerStartTime > _powerUpTime * _lockPercentage)
        {
            _locked = true;
        }

        if (Time.time - _powerStartTime > _powerUpTime)
        {
            ChangeState(_releaseState);
            return;
        }

        if (!_locked)
        {
            _entity.LookTowardsTarget(_player.transform.position);
        }
    }
}
