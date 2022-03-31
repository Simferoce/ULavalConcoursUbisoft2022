using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnShadow : Skill
{
    [SerializeField] private GameObject _shadowPrefab = null;
    [SerializeField] private float _delay = 0.0f;
    [SerializeField] private float _shadowDistance = 0.0f;
    [SerializeField] private State _disableOnStateDisabled = null;
    [SerializeField] private float _speedUpPerUse = 2;

    private Player _player = null;
    private float _lastTimeSpawn = 0.0f;

    private bool _firstUse = true;

    private List<GameObject> _minions = new List<GameObject>();

    private void Awake()
    {
        _player = GameObject.FindObjectOfType<Player>();
        if(_disableOnStateDisabled != null)
        {
            _disableOnStateDisabled.OnStateDisable += _whenInState_OnStateDisable;
        }
    }

    private void _whenInState_OnStateDisable(State state)
    {
        enabled = false;
    }

    public override bool CanUse()
    {
        return true;
    }

    public override void Use()
    {
        base.Use();
        if (_firstUse)
        {
            _firstUse = false;
        }
        else
        {
            _delay = _delay / _speedUpPerUse;
        }
        
        enabled = true;
        InvokeOnSkillFinish();
    }

    private void Update()
    {
        if(Time.time - _lastTimeSpawn > _delay)
        {
            Vector2 pos = Random.insideUnitCircle.normalized * _shadowDistance;
            _minions.Add(Instantiate(_shadowPrefab, _player.transform.position + new Vector3(pos.x, _player.transform.position.y, pos.y), Quaternion.identity));
            _lastTimeSpawn = Time.time;
        }
    }

    private void OnDestroy()
    {
        foreach(GameObject minion in _minions)
        {
            Destroy(minion);
        }

        _minions.Clear();
    }
}
