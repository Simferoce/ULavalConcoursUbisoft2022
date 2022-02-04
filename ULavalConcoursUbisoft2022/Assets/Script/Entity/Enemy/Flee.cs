using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Flee : State
{
    [SerializeField] private float _distanceStopFleeing = 0.0f;
    [SerializeField] private float _speed = 0.0f;

    [Header("Reference")]
    [SerializeField] private NavMeshAgent _navMeshAgent = null;

    [Header("State")]
    [SerializeField] private Wander _wander = null;

    private Player _player = null;
    private bool _movingBackward = false;

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
        
    }

    protected override void OnUpdate()
    {
        Vector3 playerPositionOnPlane = Vector3.ProjectOnPlane(_player.transform.position, Vector3.up);
        Vector3 aiPositionOnPlane = Vector3.ProjectOnPlane(transform.position, Vector3.up);


        if (Vector3.Distance(playerPositionOnPlane, aiPositionOnPlane) > _distanceStopFleeing)
        {
            ChangeState(_wander);
        }

        _navMeshAgent.SetDestination(FindBackwardPosition(transform.position, (aiPositionOnPlane - playerPositionOnPlane).normalized));

        //return -1;
    }

    private Vector3 FindBackwardPosition(Vector3 origin, Vector3 direction)
    {
        Vector3 position = origin;

        RaycastHit hit;
        if (Physics.Raycast(origin, direction, out hit, 3))
        {
            position = hit.point;
        }
        else
        {
            position = origin + direction;
        }
        return position;
    }
}
