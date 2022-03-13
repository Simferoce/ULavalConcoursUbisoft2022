using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Entity _entity = null;
    [SerializeField] private Animator _animator = null;
    [SerializeField] private Transform _aim;
    [SerializeField] private Vector3 DebugForward = Vector3.zero;

    private void Awake()
    {
        _entity.OnAttack += _entity_OnAttack;
    }

    private void _entity_OnAttack(Weapon.WeaponType obj)
    {
        if(obj == Weapon.WeaponType.Ranged)
        {
            _animator.SetTrigger("isAttackRange");
        }
        else if (obj == Weapon.WeaponType.Melee)
        {
            _animator.SetTrigger("isAttackingMelee");
        }
    }

    private void Update()
    {
        _animator.SetBool("isMoving", _entity.IsMoving);

        DebugForward = _entity.Root.transform.forward;
        float angle = Vector3.SignedAngle(_entity.Root.transform.forward, _entity.Translation, Vector3.up);
        _animator.SetFloat("runAngle", angle / 180);
        _animator.SetBool("isDeath", _entity.Health.IsDead());
    }

}
