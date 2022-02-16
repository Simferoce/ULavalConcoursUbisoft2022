using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CastSkill : State
{
    [SerializeField] private Skill[] _skills = null;

    [SerializeField] private State _resumeState = null;

    protected override void Init()
    {
        
    }

    protected override void OnEnter()
    {
        Skill skill = _skills.First(x => x.CanUse());
        skill.OnSkillFinish += OnSkillFinish;
        skill.Use();
    }

    private void OnSkillFinish()
    {
        ChangeState(_resumeState);
    }

    protected override void OnExit()
    {
       
    }

    protected override void OnUpdate()
    {
        
    }

    public override bool CanChangeState()
    {
        return _skills.Any(x => x.CanUse());
    }
}
