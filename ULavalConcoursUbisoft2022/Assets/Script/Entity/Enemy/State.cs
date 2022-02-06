using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
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
        newState.enabled = true;
    }

    public virtual bool CanChangeState()
    {
        return true;
    }
}
