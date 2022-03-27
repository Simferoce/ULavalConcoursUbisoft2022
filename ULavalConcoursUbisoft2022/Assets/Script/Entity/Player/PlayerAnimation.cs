using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator = null;
    [SerializeField] private float _attackMeleeRampUp = 1.0f;
    [SerializeField] private float _attackRangeRampUp = 1.125f;
    private Entity _entity = null;
    private Player _player = null;

    private void Awake()
    {
        _entity = transform.parent.GetComponentInChildren<Entity>();
        _player = GetComponentInParent<Player>();
        _entity.OnAttack += _entity_OnAttack;
        _player.InitRevive.AddListener(OnReviveInit);
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

    private void OnDestroy()
    {
        _player.InitRevive.RemoveListener(OnReviveInit);
    }

    private void OnReviveInit()
    {
        _animator.SetTrigger("Revive");
    }

    private void Update()
    {
        _animator.SetBool("isMoving", _entity.IsMoving);

        float angle = Vector3.SignedAngle(_entity.Root.transform.forward, _entity.Translation, Vector3.up);
        _animator.SetFloat("runAngle", angle / 180);
        _animator.SetBool("isDeath", _entity.Health.IsDead());

        float speedAnimToMake1Sec = _entity.WeaponHandler.WeaponData.Type == Weapon.WeaponType.Melee ? _attackMeleeRampUp : _attackRangeRampUp;
        _animator.SetFloat("attackSpeedMulti", (1 / _entity.WeaponHandler.WeaponData.GetAttackDelay(_entity.Inventory)) * speedAnimToMake1Sec);
        _animator.SetFloat("Horizontal", _entity.Translation.x);
        _animator.SetFloat("Vertical", _entity.Translation.z);
        _animator.SetBool("isDeath", _entity.Health.IsDead());

        var forwardA = _player.transform.rotation * Vector3.forward;
        var forwardB = _player.LastRot * Vector3.forward;


        // get a numeric angle for each vector, on the X-Z plane (relative to world forward)
        var angleA = Mathf.Atan2(forwardA.x, forwardA.z) * Mathf.Rad2Deg;
        var angleB = Mathf.Atan2(forwardB.x, forwardB.z) * Mathf.Rad2Deg;


        // get the signed difference in these angles
        var angleDiff = Mathf.DeltaAngle(angleA, angleB);

        if (!_player.Lock)
        {
            _animator.SetBool("RightTurn", angleDiff > 0.01f);
            _animator.SetBool("LeftTurn", angleDiff < -0.01f);
        }
        else
        {
            _animator.SetBool("RightTurn", false);
            _animator.SetBool("LeftTurn", false);
        }
    }
}
