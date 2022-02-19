using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityStrike : State
{
    [Header("Parameters")]
    [SerializeField] private float _radius = 0.0f;
    [SerializeField] private GameObject _attackPrefab = null;

    [Header("State")]
    [SerializeField] private State _returnState = null;

    private Vector3 _target = Vector3.zero;

    public float Radius { get => _radius; set => _radius = value; }
    public Vector3 Target { get => _target; set => _target = value; }

    protected override void Init()
    {
       
    }

    protected override void OnEnter()
    {
        GameObject attackObject = Instantiate(_attackPrefab, Vector3.ProjectOnPlane(_target, Vector3.up) + new Vector3(0,0.5f,0), Quaternion.identity);
        Attack attack = attackObject.GetComponent<Attack>();
        attack.Team = Entity.Team.Foe;
        SphereCollider collider = attackObject.GetComponentInChildren<SphereCollider>();
        collider.radius = _radius;

        ChangeState(_returnState);
    }

    protected override void OnExit()
    {
        
    }

    protected override void OnUpdate()
    {
        
    }
}
