using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CastSkill : State
{
    [SerializeField] private Skill[] _skills = null;

    [SerializeField] private State _resumeState = null;

    private bool _casting = false;

    protected override void Init()
    {
        
    }

    protected override void OnEnter()
    {

    }

    private void OnSkillFinish(Skill skill)
    {
        _casting = false;
        ChangeState(_resumeState);
    }

    protected override void OnExit()
    {
       
    }

    protected override void OnUpdate()
    {
        if(!_casting)
        {
            TryCastSkill();
        }
    }

    public override bool CanChangeState()
    {
        return _skills.Any(x => x.CanUse());
    }

    private void TryCastSkill()
    {
        Skill skill = _skills.FirstOrDefault(x => x.CanUse());
        if(skill != null)
        {
            _casting = true;
            skill.OnSkillFinish += OnSkillFinish;

            skill.Use();
        }
    }
}
