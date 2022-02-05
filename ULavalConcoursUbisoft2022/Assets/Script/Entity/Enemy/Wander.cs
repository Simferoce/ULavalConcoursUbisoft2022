using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wander : State
{
    [Header("Parameters")]
    [SerializeField] private float _distanceDetectPlayer = 0.0f;
    [SerializeField] private float _MaxRangeWander = 0.0f;
    [SerializeField] private Vector2 delayWander = Vector2.zero;
    [SerializeField] private float _speed = 0.0f;

    [Header("Reference")]
    [SerializeField] private NavMeshAgent _navMeshAgent = null;
    [SerializeField] private Entity _entity = null;

    [Header("State")]
    [SerializeField] private State _whenPlayerToClose = null;

    private Player _player = null;
    private float _lastWander = 0.0f;
    private float _currentOffSetWander = 0.0f;

    private Vector3 _origin = Vector3.zero;


    private Vector3 FindWanderPosition()
    {
        Vector2 randomInCircle = UnityEngine.Random.insideUnitCircle * _MaxRangeWander;
        return _origin + new Vector3(randomInCircle.x, 0, randomInCircle.y);
    }

    protected override void OnEnter()
    {
        _currentOffSetWander = UnityEngine.Random.Range(delayWander.x, delayWander.y);
        _navMeshAgent.speed = _speed;
    }

    protected override void OnExit()
    {
        
    }

    protected override void OnUpdate()
    {
        Vector3 playerPositionOnPlane = Vector3.ProjectOnPlane(_player.transform.position, Vector3.up);
        Vector3 aiPositionOnPlane = Vector3.ProjectOnPlane(transform.position, Vector3.up);

        if (Vector3.Distance(playerPositionOnPlane, aiPositionOnPlane) < _distanceDetectPlayer && _entity.Sees(_player.transform.position))
        {
            ChangeState(_whenPlayerToClose);
            return;
        }

        if (Time.time - _currentOffSetWander > _lastWander)
        {
            Vector3 destination = FindWanderPosition();
            _lastWander = Time.time;
            _currentOffSetWander = UnityEngine.Random.Range(delayWander.x, delayWander.y);

            RaycastHit hit;
            if (Physics.Raycast(_origin, (destination - _origin).normalized, out hit, (destination - _origin).magnitude))
            {
                destination = hit.point;
            }

            _navMeshAgent.SetDestination(destination);
        }
    }

    protected override void Init()
    {
        _player = FindObjectOfType<Player>();
        _origin = transform.position;
    }
}
