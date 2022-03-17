using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator = null;
    private Entity _entity = null;

    private void Awake()
    {
        _entity = transform.parent.GetComponentInChildren<Entity>();
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

        float angle = Vector3.SignedAngle(_entity.Root.transform.forward, _entity.Translation, Vector3.up);
        _animator.SetFloat("runAngle", angle / 180);
        _animator.SetBool("isDeath", _entity.Health.IsDead());


        _animator.SetFloat("attackSpeedMulti", (1 / _entity.WeaponHandler.WeaponData.GetAttackDelay(_entity.Inventory)));
    }

}
