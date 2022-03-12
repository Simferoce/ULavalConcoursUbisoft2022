using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    public event Action<State> OnStateDisable;
    public event Action<State> OnStateEnable;

    protected abstract void Init();
    protected abstract void OnEnter();
    protected abstract void OnUpdate();
    protected abstract void OnExit();

    private void Awake()
    {
        Init();
    }

    private bool _hasEnter = false;

    private void Update()
    {
        if(!_hasEnter)
        {
            _hasEnter = true;
            OnEnter();
            OnStateEnable?.Invoke(this);
        }
        OnUpdate();
    }

    private void OnDisable()
    {
        _hasEnter = false;
        OnExit();
    }

    protected void ChangeState(State newState)
    {
        this.enabled = false;
        OnStateDisable?.Invoke(this);

        if (newState != null)
        {
            newState.enabled = true;
        }  
    }

    public void EnableState()
    {
        this.enabled = true;
    }

    public void ForceDisableState()
    {
        this.enabled = false;
    }

    public virtual bool CanChangeState()
    {
        return true;
    }
}
