using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GetCloserToPlayer : State
{
    [Header("Parameters")]
    [SerializeField] private float _distance = 0.0f;
    [Tooltip("Speed < 0 == do not override speed")]
    [SerializeField] private float _speed = 0.0f;

    [Header("Reference")]
    [SerializeField] private NavMeshAgent _navMeshAgent = null;
    [SerializeField] private Entity _entity = null;

    [Header("State")]
    [SerializeField] private State _transition = null;

    private Player _player = null;

    protected override void Init()
    {
        _player = FindObjectOfType<Player>();
    }

    protected override void OnEnter()
    {
        _navMeshAgent.isStopped = false;
        if(_speed > 0)
        {
            _navMeshAgent.speed = _speed;
        }
    }

    protected override void OnExit()
    {
        
    }

    protected override void OnUpdate()
    {
        Vector3 playerPositionOnPlane = Vector3.ProjectOnPlane(_player.transform.position, Vector3.up);
        Vector3 aiPositionOnPlane = Vector3.ProjectOnPlane(transform.position, Vector3.up);

        float distance = Vector3.Distance(playerPositionOnPlane, aiPositionOnPlane);

        if (distance < _distance + 0.1f)
        {
            _navMeshAgent.isStopped = true;
            ChangeState(_transition);
        }

        Vector3 destination = transform.position;

        bool seePlayer = _entity.Sees(_player.transform.position);

        if (seePlayer)
        {
            if (distance > _distance)
            {
                destination = (aiPositionOnPlane - playerPositionOnPlane).normalized * _distance + playerPositionOnPlane + new Vector3(0, transform.position.y, 0);
            }
        }
        else
        {
            destination = _player.transform.position;
        }

        _entity.LookTowardsTarget(_player.transform.position);
        _navMeshAgent.SetDestination(destination);
    }

    private void OnDestroy()
    {
        _navMeshAgent.isStopped = true;
    }
}
