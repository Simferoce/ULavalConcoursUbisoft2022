using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranscendentExpressionSkill : Skill
{
    [SerializeField] private Awakening _awakening;
    [SerializeField] private float _energyPerShot = 0.0f;
    [SerializeField] private GameObject _projectile = null;
    [SerializeField] private Entity _entity = null;
    [SerializeField] private Vector2 _delayBetweenEachShot = Vector2.zero;
    [SerializeField] private float _heightSpawn = 0.0f;

    public override bool CanUse()
    {
        return true;
    }

    public override void Use()
    {
        StartCoroutine(DepleteEnergy());
    }

    private IEnumerator DepleteEnergy()
    {
        while (_awakening.Energy >= _energyPerShot)
        {
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            Instantiate(_projectile, new Vector3(_entity.transform.position.x, _heightSpawn, _entity.transform.position.z), Quaternion.LookRotation(new Vector3(randomDirection.x, 0, randomDirection.y), Vector3.up));
            _awakening.Energy -= _energyPerShot;

            yield return new WaitForSeconds(Random.Range(_delayBetweenEachShot.x, _delayBetweenEachShot.y));
        }

        _awakening.Energy = 0;

        InvokeOnSkillFinish();
    }
}
