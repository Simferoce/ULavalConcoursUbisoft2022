using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReleaseAttack : State
{
    [Header("Parameters")]

    [Header("Reference")]
    [SerializeField] private Entity _entity = null;

    [Header("State")]
    [SerializeField] private Wander _wander = null;

    private Player _player = null;

    protected override void Init()
    {
        _player = FindObjectOfType<Player>();
    }

    protected override void OnEnter()
    {
        _entity.Attack();
        ChangeState(_wander);
    }

    protected override void OnExit()
    {
        
    }

    protected override void OnUpdate()
    {
        
    }
}
