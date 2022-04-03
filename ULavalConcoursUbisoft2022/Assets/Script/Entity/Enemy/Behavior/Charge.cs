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

    [Header("State")]
    [SerializeField] private State _chargeStateFinish = null;

    public float Speed { get => _speed; set => _speed = value; }
    public float MaxRange { get => _maxRange; set => _maxRange = value; }
    public float Acceleration { get => _acceleration; set => _acceleration = value; }

    private float _originAcceleration = 0.0f;
    private Vector3 _destination;
    private float _currentTravelDistance = 0.0f;
    private float _travelDistance = 0.0f;

    protected override void Init()
    {
        
    }


    protected override void OnEnter()
    {
        _currentTravelDistance = 0.0f;
        _navMeshAgent.enabled = false;

        //Debug.DrawLine(_navMeshAgent.transform.position, _navMeshAgent.transform.position + _navMeshAgent.transform.forward * _maxRange, Color.blue, 2);

        RaycastHit hit;
        if (Physics.Raycast(_navMeshAgent.transform.position, _navMeshAgent.transform.forward, out hit, _maxRange, LayerMask.GetMask("Wall")))
        {
            _destination = hit.point - _navMeshAgent.transform.forward;
        }
        else
        {
            _destination = _navMeshAgent.transform.position + _navMeshAgent.transform.forward * _maxRange;
        }

        Vector3 entityOnPlane = Vector3.ProjectOnPlane(_entity.transform.position, Vector3.up);
        Vector3 destinationOnPlane = Vector3.ProjectOnPlane(_destination, Vector3.up);

        _entity.LookTowardsTarget(_destination);
        _travelDistance = Vector3.Distance(entityOnPlane, destinationOnPlane);

    }

    protected override void OnExit()
    {

    }

    protected override void OnUpdate()
    {
        if (_currentTravelDistance > _travelDistance)
        {
            _navMeshAgent.enabled = true;
            _entity.Teleport(_destination);
            ChangeState(_chargeStateFinish);
        }
        else
        {
            _entity.Move(_entity.transform.forward * Time.deltaTime * _speed);
            _currentTravelDistance += Time.deltaTime * _speed;
        }
    }
}
