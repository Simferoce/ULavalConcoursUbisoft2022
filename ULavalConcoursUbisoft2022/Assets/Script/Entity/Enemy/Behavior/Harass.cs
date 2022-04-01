using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Harass : State
{
    [Header("Parameters")]
    [SerializeField] private float _speed = 0.0f;
    [SerializeField] private float _stopChasingPlayerRange = 0.0f;
    [SerializeField] private float _distanceKeptBetweenItselfAndPlayer = 0.0f;
    [SerializeField] private float _attackDelay = 0.5f;

    [Header("Reference")]
    [SerializeField] private NavMeshAgent _navMeshAgent = null;
    [SerializeField] private Entity _entity = null;

    [Header("State")]
    [SerializeField] private State _wander = null;
    [SerializeField] private State _flee = null;

    private Player _player = null;

    private bool _attacking = false;

    protected override void Init()
    {
        _player = FindObjectOfType<Player>();
    }

    protected override void OnEnter()
    {
        _navMeshAgent.speed = _speed;
    }

    protected override void OnExit()
    {
        StopAllCoroutines();
    }

    protected override void OnUpdate()
    {
        Vector3 playerPositionOnPlane = Vector3.ProjectOnPlane(_player.transform.position, Vector3.up);
        Vector3 aiPositionOnPlane = Vector3.ProjectOnPlane(transform.position, Vector3.up);

        float distance = Vector3.Distance(playerPositionOnPlane, aiPositionOnPlane);

        Vector3 destination = transform.position;

        bool seePlayer = _entity.Sees(_player.transform.position);

        if (seePlayer)
        {
            if(distance > _distanceKeptBetweenItselfAndPlayer)
            {
                destination = (aiPositionOnPlane - playerPositionOnPlane).normalized * _distanceKeptBetweenItselfAndPlayer + playerPositionOnPlane + new Vector3(0, transform.position.y, 0);
            }
        }
        else
        {
            destination = _player.transform.position;
        }

        if(!_attacking)
        {
            _navMeshAgent.SetDestination(destination);
        }


        _entity.LookTowardsTarget(_player.transform.position);
        if (seePlayer && _entity.AttackRange() > distance && _entity.CanAttack())
        {
            if(!_attacking)
            {
                _navMeshAgent.isStopped = true;
                _attacking = true;
                StartCoroutine(Attack());
            }
        }

        if(_flee != null && _flee.CanChangeState())
        {
            ChangeState(_flee);
        }

        if (_wander != null &&_wander.CanChangeState() && Vector3.Distance(playerPositionOnPlane, aiPositionOnPlane) > _stopChasingPlayerRange)
        {
            ChangeState(_wander);
        }
    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(_attackDelay);
        _entity.Attack();
        _attacking = false;
        _navMeshAgent.isStopped = false;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
