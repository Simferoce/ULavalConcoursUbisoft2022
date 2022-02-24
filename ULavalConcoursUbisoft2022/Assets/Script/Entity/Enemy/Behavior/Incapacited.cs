using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Incapacited : State
{
    [Header("Parameters")]
    [SerializeField] private float _time = 0.0f;

    [Header("Reference")]
    [SerializeField] private NavMeshAgent _navMeshAgent = null;
    [SerializeField] private BubbleText _bubbleText = null;

    [Header("State")]
    [SerializeField] private State _resumeState = null;

    public float Time { get => _time; set => _time = value; }

    protected override void Init()
    {
        
    }

    protected override void OnEnter()
    {
        _navMeshAgent.isStopped = true;

        if(Time > 0)
        {
            _bubbleText.ShowMessage(2, Time);
        }
        
        StartCoroutine(WaitIncapicated());
    }

    protected override void OnExit()
    {
        
    }

    protected override void OnUpdate()
    {
        
    }

    private IEnumerator WaitIncapicated()
    {
        yield return new WaitForSeconds(_time);

        _navMeshAgent.isStopped = false;
        ChangeState(_resumeState);
    }
}
