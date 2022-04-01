using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddSummonState : State
{
    [Header("Parameters")]
    [SerializeField] private int _setNumbersOfSummonTo = 0;
    [SerializeField] private float _percentageTriggerState = 0.0f;

    [Header("Reference")]
    [SerializeField] private SummonSkill _summonSkill = null;
    [SerializeField] private BubbleText _bubbleText = null;
    [SerializeField] private Health _health = null;

    [Header("State")]
    [SerializeField] private State _nextState = null;

    private bool _messageShown = false;

    public override bool CanChangeState()
    {
        return _health.Percentage < _percentageTriggerState;
    }

    protected override void Init()
    {

    }

    protected override void OnEnter()
    {
        _summonSkill.NumberToSummon = _setNumbersOfSummonTo;

        if(!_messageShown)
        {
            _bubbleText.ShowMessage(1);
            _messageShown = true;
        }

        ChangeState(_nextState);
    }

    protected override void OnExit()
    {

    }

    protected override void OnUpdate()
    {

    }
}
