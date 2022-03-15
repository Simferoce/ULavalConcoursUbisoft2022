using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAttack : State
{
    [System.Serializable]
    public class RandomAttackChoose
    {
        public float Percentage = 0.0f;
        public State State = null;
    }

    public List<RandomAttackChoose> _attacks = new List<RandomAttackChoose>();

    protected override void Init()
    {

    }

    protected override void OnEnter()
    {

    }

    protected override void OnExit()
    {

    }

    protected override void OnUpdate()
    {
        float random = Random.Range(0.0f, 1.0f);
        float sum = 0.0f;

        for (int i = 0; i < _attacks.Count; ++i)
        {
            sum += _attacks[i].Percentage;
            if(random < sum)
            {
                ChangeState(_attacks[i].State);
                return;
            }
        }
    }
}
