using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class State : MonoBehaviour
{
    [SerializeField] private UnityEvent OnStateEnableHook;
    [SerializeField] private UnityEvent OnStateDisableHook;

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
            OnStateEnableHook?.Invoke();
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
        OnStateDisableHook?.Invoke();

        if (newState != null)
        {
            newState.enabled = true;
        }  
    }

    public void EnableState()
    {
        this.enabled = true;
    }

    public virtual void ForceDisableState()
    {
        this.enabled = false;
    }

    public virtual bool CanChangeState()
    {
        return true;
    }
}
