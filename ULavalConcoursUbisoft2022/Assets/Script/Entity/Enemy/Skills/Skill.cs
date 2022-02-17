using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    public event Action<Skill> OnSkillFinish;

    public abstract bool CanUse();
    public abstract void Use();

    public void InvokeOnSkillFinish()
    {
        OnSkillFinish?.Invoke(this);
    }
}
