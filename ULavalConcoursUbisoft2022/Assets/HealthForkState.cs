using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HealthForkState : State
{
    [System.Serializable]
    public class StateToForkAtHealth
    {
        public bool HasBeenUsed;
        public State State;
        public float Threashold;
    }

    [SerializeField] private List<StateToForkAtHealth> _stateToForkAtHealths = null;
    [SerializeField] private State _default = null;
    [SerializeField] private Health _health = null;

    protected override void Init()
    {

    }

    protected override void OnEnter()
    {
        StateToForkAtHealth transition = _stateToForkAtHealths.FirstOrDefault(x => !x.HasBeenUsed && _health.Percentage < x.Threashold);
        if(transition != null)
        {
            transition.HasBeenUsed = true;
            ChangeState(transition.State);
        }
        else
        {
            ChangeState(_default);
        }
    }

    protected override void OnExit()
    {

    }

    protected override void OnUpdate()
    {

    }
}
