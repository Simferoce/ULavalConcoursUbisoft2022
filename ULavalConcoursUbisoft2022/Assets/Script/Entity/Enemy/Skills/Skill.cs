using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Skill : MonoBehaviour
{
    public event Action<Skill> OnSkillFinish;
    public UnityEvent OnSkillFinishHook;

    public abstract bool CanUse();
    public abstract void Use();

    public void InvokeOnSkillFinish()
    {
        OnSkillFinish?.Invoke(this);
        OnSkillFinishHook?.Invoke();
    }
}
