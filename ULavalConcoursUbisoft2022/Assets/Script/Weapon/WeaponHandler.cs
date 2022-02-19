using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private Weapon _startingWeapon = null;

    private Weapon _weaponData = null;
    private Attack _attack = null;
    private float _colliderRange = 0.0f;

    private void Awake()
    {
        SetWeapon(_startingWeapon);
    }

    public void SetWeapon(Weapon weapon)
    {
        _weaponData = weapon;
        _attack = weapon.Collider.GetComponent<Attack>();

        BoxCollider attackBoxCollider = weapon.Collider.GetComponentInChildren<BoxCollider>();

        if (_attack.Speed == 0.0f)
        {
            if (attackBoxCollider != null)
            {
                _colliderRange = attackBoxCollider.transform.position.z + attackBoxCollider.size.z / 2;
            }
            else
            {
                SphereCollider sphereCollider = weapon.Collider.GetComponentInChildren<SphereCollider>();
                if (sphereCollider != null)
                {
                    _colliderRange = sphereCollider.radius / 2;
                }
                else
                {
                    _colliderRange = 0.0f;
                }
            }
        }
        else
        {
            TimedLife timedLife = _attack.GetComponent<TimedLife>();
            _colliderRange = _attack.Speed * timedLife.TimeToLive;
        }

       
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
        return _colliderRange;
    }
}
