using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneTimeStrikeSkill : Skill
{
    [Header("Parameters")]
    [SerializeField] private float _cooldown;
    [SerializeField] private float _playerDetectDirection = 0.0f;
    [SerializeField] private float _randomFactor = 0.0f;

    [Header("Override Parameters")]
    [SerializeField] private float _range = 0.0f;

    [Header("Reference")]
    [SerializeField] private Entity _entity = null;

    [Header("States")]
    [SerializeField] private PowerUp _powerUp;
    [SerializeField] private OneTimeStrike _oneTimeStrike = null;
    [SerializeField] private GameObject _indicator = null;

    private float _lastTimeUsed = 0.0f;

    public PowerUp PowerUp { get => _powerUp; set => _powerUp = value; }

    private AOELineIndicator _instancedIndicator = null;
    private Player _player = null;

    private void Awake()
    {
        _player = GameObject.FindObjectOfType<Player>();
        _powerUp.OnLock += _powerUp_OnLock;
        _oneTimeStrike.Range = _range;
        _oneTimeStrike.OnStateDisable += _proximityStrike_OnStateDisable;
        _lastTimeUsed = -_cooldown - _powerUp.PowerUpTime;
        _entity.Health.OnDeath.AddListener(Health_OnDeath);
    }

    private void _powerUp_OnLock()
    {
        _oneTimeStrike.Target = _entity.transform.position;
    }

    private void Health_OnDeath(Health obj)
    {
        _entity.Health.OnDeath.RemoveListener(Health_OnDeath);
    }

    private void OnDestroy()
    {
        if (_instancedIndicator != null)
        {
            _instancedIndicator.Destroy();
        }

        _oneTimeStrike.OnStateDisable -= _proximityStrike_OnStateDisable;
        _powerUp.OnLock -= _powerUp_OnLock;
    }

    private void _proximityStrike_OnStateDisable(State state)
    {
        InvokeOnSkillFinish();
    }

    public override bool CanUse()
    {
        return Time.time - _lastTimeUsed - _powerUp.PowerUpTime > _cooldown;
    }

    public override void Use()
    {
        base.Use();
        if (_entity.Health.IsDead())
        {
            return;
        }

        Vector2 random = Random.insideUnitCircle.normalized;
        Vector3 target = _player.transform.position + _player.GetComponentInChildren<Entity>().Translation.normalized * _playerDetectDirection + new Vector3(random.x, 0, random.y) * _randomFactor;
        _entity.LookTowardsTarget(target);
        _oneTimeStrike.Target = target;

        GameObject gameObject = Instantiate(_indicator, _entity.transform.position, Quaternion.identity);
        _instancedIndicator = gameObject.GetComponentInChildren<AOELineIndicator>();
        _instancedIndicator.Init(_powerUp.PowerUpTime, new Vector3(2, _range), _entity.transform);

        _lastTimeUsed = Time.time;
        _powerUp.EnableState();
    }
}
