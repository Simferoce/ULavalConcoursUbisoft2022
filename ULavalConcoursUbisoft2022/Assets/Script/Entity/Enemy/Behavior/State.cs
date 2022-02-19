using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    public event Action OnStateDisable;

    protected abstract void Init();
    protected abstract void OnEnter();
    protected abstract void OnUpdate();
    protected abstract void OnExit();

    private void Awake()
    {
        Init();
    }

    private void OnEnable()
    {
        OnEnter();
    }

    private void Update()
    {
        OnUpdate();
    }

    private void OnDisable()
    {
        OnExit();
    }

    protected void ChangeState(State newState)
    {
        this.enabled = false;
        OnStateDisable?.Invoke();

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
