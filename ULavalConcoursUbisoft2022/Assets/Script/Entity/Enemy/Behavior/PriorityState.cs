using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PriorityState : State
{
    public PriorityStateEntry[] _priorityStates = null;

    [System.Serializable]
    public struct PriorityStateEntry
    {
        public int Priority;
        public State State;
    }

    protected override void Init()
    {

    }

    protected override void OnEnter()
    {

    }

    protected override void OnExit()
    {

    }

    protected override void OnUpdate()
    {
        _priorityStates = _priorityStates.OrderBy(x => x.Priority).ToArray();

        for (int i = 0; i < _priorityStates.Length; ++i)
        {
            if(_priorityStates[i].State.CanChangeState())
            {
                ChangeState(_priorityStates[i].State);
                break;
            }
        }
    }
}
