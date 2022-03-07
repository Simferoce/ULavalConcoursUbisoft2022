using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duplicate : State
{
    [Header("Parameters")]
    [SerializeField] private GameObject _clone = null;
    [SerializeField] private List<Transform> positions = new List<Transform>();

    [Header("Reference")]
    [SerializeField] private Entity _entity = null;

    [Header("State")]
    [SerializeField] private State _nextState = null;

    private int _alive = 0;
    protected override void Init()
    {
        
    }

    protected override void OnEnter()
    {
        _entity.Vanish();

        _alive = 0;
        foreach (Transform transform in positions)
        {
            GameObject clone = Instantiate(_clone, transform.position, transform.rotation);
            clone.GetComponentInChildren<Health>().OnDeath.AddListener(Duplicate_OnDeath);
            _alive++;
        }
    }

    private void Duplicate_OnDeath(Health obj)
    {
        obj.OnDeath.RemoveListener(Duplicate_OnDeath);
        _alive--;
    }

    protected override void OnExit()
    {
        _entity.Appear();
    }

    protected override void OnUpdate()
    {
        if(_alive == 0)
        {
            ChangeState(_nextState);
        }
    }
}
