using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneTimeStrike : State
{
    [Header("Parameters")]
    [SerializeField] private float _range = 0.0f;
    [SerializeField] private GameObject _attackPrefab = null;
    [SerializeField] private Entity _entity = null;

    [Header("State")]
    [SerializeField] private State _returnState = null;

    private Vector3 _target = Vector3.zero;

    public float Range { get => _range; set => _range = value; }
    public Vector3 Target { get => _target; set => _target = value; }

    protected override void Init()
    {
        //GameObject attackObject = Instantiate(_attackPrefab, Vector3.ProjectOnPlane(_target, Vector3.up) + new Vector3(0, 0.5f, 0), Quaternion.identity);
        //Attack attack = attackObject.GetComponent<Attack>();
        //attack.Team = Entity.Team.Foe;
        //BoxCollider collider = attackObject.GetComponentInChildren<BoxCollider>();
        //collider.size = new Vector3(collider.size.x, collider.size.y, _range);
        //collider.center = new Vector3(collider.center.x, collider.center.y, _range / 2);
    }

    protected override void OnEnter()
    {
        GameObject attackObject = Instantiate(_attackPrefab, Vector3.ProjectOnPlane(_target, Vector3.up) + new Vector3(0, 0.5f, 0), _entity.transform.rotation);
        Attack attack = attackObject.GetComponent<Attack>();
        attack.Team = Entity.Team.Foe;
        BoxCollider collider = attackObject.GetComponentInChildren<BoxCollider>();
        collider.size = new Vector3(collider.size.x, collider.size.y, _range);
        collider.center = new Vector3(collider.center.x, collider.center.y, _range / 2);

        ChangeState(_returnState);
    }

    protected override void OnExit()
    {

    }

    protected override void OnUpdate()
    {

    }
}
