using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Charge : State
{
    [Header("Parameters")]
    [SerializeField] private float _speed = 0.0f;
    [SerializeField] private float _acceleration = 0.0f;
    [SerializeField] private float _maxRange = 0.0f;

    [Header("Reference")]
    [SerializeField] private NavMeshAgent _navMeshAgent = null;
    [SerializeField] private Entity _entity = null;
    [SerializeField] private Attack _attack = null;

    [Header("State")]
    [SerializeField] private State _chargeStateFinish = null;

    public float Speed { get => _speed; set => _speed = value; }
    public float MaxRange { get => _maxRange; set => _maxRange = value; }
    public float Acceleration { get => _acceleration; set => _acceleration = value; }

    private float _originAcceleration = 0.0f;
    private Vector3 _destination;

    protected override void Init()
    {
        
    }


    protected override void OnEnter()
    {
        _attack.gameObject.SetActive(true);

        _navMeshAgent.isStopped = false;

        _originAcceleration = _navMeshAgent.acceleration;

        _navMeshAgent.speed = _speed;
        _navMeshAgent.acceleration = 100;

        Debug.DrawLine(_navMeshAgent.transform.position, _navMeshAgent.transform.position + _navMeshAgent.transform.forward * _maxRange, Color.blue, 5);

        RaycastHit hit;
        if (Physics.Raycast(_navMeshAgent.transform.position, _navMeshAgent.transform.forward, out hit, _maxRange, LayerMask.GetMask("Wall")))
        {
            _destination = hit.point;
        }
        else
        {
            _destination = _navMeshAgent.transform.position + _navMeshAgent.transform.forward * _maxRange;

        }

        _entity.LookTowardsTarget(_destination);
        _navMeshAgent.SetDestination(_destination);

    }

    protected override void OnExit()
    {
        _attack.gameObject.SetActive(false);
        _navMeshAgent.acceleration = _originAcceleration;
    }

    protected override void OnUpdate()
    {
        if (_navMeshAgent.remainingDistance == 0)
        {
            ChangeState(_chargeStateFinish);
        }
    }
}
