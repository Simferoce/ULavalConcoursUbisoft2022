using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityStrike : State
{
    [Header("Parameters")]
    [SerializeField] private float _radius = 0.0f;
    [SerializeField] private GameObject _attackPrefab = null;

    [Header("Reference")]
    [SerializeField] private Entity _entity = null;

    [Header("State")]
    [SerializeField] private State _returnState = null;

    public float Radius { get => _radius; set => _radius = value; }

    protected override void Init()
    {
       
    }

    protected override void OnEnter()
    {
        GameObject attackObject = Instantiate(_attackPrefab, Vector3.ProjectOnPlane(_entity.transform.position, Vector3.up) + new Vector3(0,0.5f,0), Quaternion.identity);
        AttackTimed attack = attackObject.GetComponent<AttackTimed>();
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
