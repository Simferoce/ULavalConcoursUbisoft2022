using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TranscendentExpressionSkill : Skill
{
    [Header("Parameters")]
    [SerializeField] private int _numberOfProjectile = 0;
    [SerializeField] private Vector2 _delayBetweenEachShot = Vector2.zero;
    [SerializeField] private float _heightSpawn = 0.0f;

    [Header("Reference")]
    [SerializeField] private GameObject _projectile = null;
    [SerializeField] private Entity _entity = null;

    public UnityEvent OnShoot;

    public List<GameObject> _projectiles = new List<GameObject>();

    public override bool CanUse()
    {
        return true;
    }

    public override void Use()
    {
        base.Use();
        StartCoroutine(DepleteEnergy());
    }

    private IEnumerator DepleteEnergy()
    {
        for (int i = 0; i < _numberOfProjectile; ++i)
        {
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            _projectiles.Add(Instantiate(_projectile, new Vector3(_entity.transform.position.x, _heightSpawn, _entity.transform.position.z), Quaternion.LookRotation(new Vector3(randomDirection.x, 0, randomDirection.y), Vector3.up)));
            OnShoot?.Invoke();
            yield return new WaitForSeconds(Random.Range(_delayBetweenEachShot.x, _delayBetweenEachShot.y));
        }

        InvokeOnSkillFinish();
    }

    public void OnDestroy()
    {
        StopAllCoroutines();

        foreach(GameObject obj in _projectiles)
        {
            Destroy(obj);
        }

        _projectiles.Clear();
    }
}
