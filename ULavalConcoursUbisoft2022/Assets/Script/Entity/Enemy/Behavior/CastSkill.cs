using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CastSkill : State
{
    [SerializeField] private Skill[] _skills = null;

    [SerializeField] private State _resumeState = null;

    private bool _casting = false;
    private Skill _skill = null;
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
        _skill = _skills.FirstOrDefault(x => x.CanUse());
        if(_skill != null)
        {
            _casting = true;
            _skill.OnSkillFinish += OnSkillFinish;

            _skill.Use();
        }
    }

    public override void ForceDisableState()
    {
        base.ForceDisableState();
        _casting = false;
        _skill.ForceDisable();
    }
}
