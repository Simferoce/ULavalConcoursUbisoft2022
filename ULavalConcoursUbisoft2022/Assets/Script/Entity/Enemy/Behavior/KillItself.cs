using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillItself : State
{
    [SerializeField] private Entity _entity = null;

    protected override void Init()
    {
        
    }

    protected override void OnEnter()
    {
        _entity.Health.Kill();
    }

    protected override void OnExit()
    {
        
    }

    protected override void OnUpdate()
    {
       
    }
}
