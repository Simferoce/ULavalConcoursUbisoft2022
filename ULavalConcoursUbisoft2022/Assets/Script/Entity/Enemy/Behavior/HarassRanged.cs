using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HarassRanged : State
{
    [Header("Parameters")]
    [SerializeField] private float _speed = 0.0f;
    [SerializeField] private float _stopChasingPlayerRange = 0.0f;
    [SerializeField] private float _distanceKeptBetweenItselfAndPlayer = 0.0f;
    [SerializeField] private float _randomMove = 0.0f;
    [SerializeField] private float _speedRandom = 0.0f;

    [Header("Reference")]
    [SerializeField] private NavMeshAgent _navMeshAgent = null;
    [SerializeField] private Entity _entity = null;

    [Header("State")]
    [SerializeField] private State _wander = null;
    [SerializeField] private State _flee = null;

    private Player _player = null;

    protected override void Init()
    {
        _player = FindObjectOfType<Player>();
    }

    protected override void OnEnter()
    {
        _navMeshAgent.speed = _speed;
        _hasReachedDestination = false;
    }

    protected override void OnExit()
    {

    }

    private Vector3 _randomPos = Vector2.zero;
    private bool _hasReachedDestination = false;
    protected override void OnUpdate()
    {
        Vector3 playerPositionOnPlane = Vector3.ProjectOnPlane(_player.transform.position, Vector3.up);
        Vector3 aiPositionOnPlane = Vector3.ProjectOnPlane(transform.position, Vector3.up);

        float distance = Vector3.Distance(playerPositionOnPlane, aiPositionOnPlane);

        Vector3 destination = transform.position;

        bool seePlayer = _entity.Sees(_player.transform.position);

        if (!_hasReachedDestination && distance < _distanceKeptBetweenItselfAndPlayer + 0.1f)
        {
            _hasReachedDestination = true;
            _navMeshAgent.speed = _randomMove;
        }
        else if(distance > _distanceKeptBetweenItselfAndPlayer + _randomMove)
        {
            _hasReachedDestination = false;
            _navMeshAgent.speed = _speed;
        }

        Vector3 randomPos = Vector3.ProjectOnPlane(_randomPos, Vector3.up);
        if (_hasReachedDestination && _navMeshAgent.remainingDistance == 0)
        {
            FindRandomPosition();
        }

        if (seePlayer)
        {
            if(!_hasReachedDestination)
            {
                destination = (aiPositionOnPlane - playerPositionOnPlane).normalized * _distanceKeptBetweenItselfAndPlayer + playerPositionOnPlane + new Vector3(0, transform.position.y, 0);
            }
            else
            {
                destination = _randomPos;
            }
        }
        else
        {
            destination = _player.transform.position;
        }

        _navMeshAgent.SetDestination(destination);

        _entity.LookTowardsTarget(_player.transform.position);
        if (seePlayer && _entity.AttackRange() > distance && _entity.CanAttack())
        {
            _entity.Attack();
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

    private void FindRandomPosition()
    {
        Vector2 random = Random.insideUnitCircle.normalized * _randomMove;
        _randomPos = _entity.transform.position + new Vector3(random.x, _entity.transform.position.y, random.y);

        RaycastHit hit;
        if (Physics.Raycast(_navMeshAgent.transform.position, (_navMeshAgent.transform.position - _randomPos), out hit, (_navMeshAgent.transform.position - _randomPos).magnitude, LayerMask.GetMask("Wall")))
        {
            _randomPos = hit.point;
        }
    }
}
