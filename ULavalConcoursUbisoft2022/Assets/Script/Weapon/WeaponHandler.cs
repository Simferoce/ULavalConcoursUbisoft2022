using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private Weapon _startingWeapon = null;

    private Weapon _weaponData = null;
    private Attack _attack = null;
    private BoxCollider _attackCollider = null;

    private void Awake()
    {
        SetWeapon(_startingWeapon);
    }

    public void SetWeapon(Weapon weapon)
    {
        _weaponData = weapon;
        _attack = weapon.Collider.GetComponent<Attack>();
        _attackCollider = weapon.Collider.GetComponentInChildren<BoxCollider>();
    }

    private float _lastTimeUsed = 0.0f;

    public void Use(Vector3 origin, Vector3 direction, Entity.Team team)
    {
        GameObject attackCollider = Instantiate(_weaponData.Collider, origin, Quaternion.LookRotation(direction, Vector3.up));
        attackCollider.GetComponent<Attack>().Team = team;
        _lastTimeUsed = Time.time;
    }

    public bool CanUse()
    {
        return Time.time - _weaponData.DelayAttack > _lastTimeUsed;
    }

    public float GetRange()
    {
        if (_attack.Speed != 0)
        {
            return _attack.Speed * _attack.TimeToLive;
        }
        else
        {
            return _attackCollider.transform.position.z +_attackCollider.size.z / 2;
        }
    }
}
