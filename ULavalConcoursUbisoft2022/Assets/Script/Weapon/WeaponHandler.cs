using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private Weapon _startingWeapon = null;
    [SerializeField] private Inventory _inventory = null;

    private Weapon _weaponData = null;
    private Attack _attack = null;
    private float _colliderRange = 0.0f;

    private float _lastTimeUsed = 0.0f;
    private float _lastRandom = 0.0f;
    private Inventory inventory;

    //For debug purpose
    private float _attackSpeed = 0.0f;

    private void Awake()
    {
        SetWeapon(_startingWeapon);
        inventory = _inventory;
    }

    public void SetWeapon(Weapon weapon)
    {
        _weaponData = weapon;
        _attack = weapon.Collider.GetComponent<Attack>();

        _lastRandom = _weaponData.RandomDelay * Random.Range(0.0f, 1.0f);
        _lastTimeUsed = Time.time - _weaponData.GetAttackDelay(inventory);

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

    public void Use(Vector3 origin, Vector3 direction, Entity.Team team, Inventory inventory)
    {
        GameObject attackCollider = Instantiate(_weaponData.Collider, origin, Quaternion.LookRotation(direction, Vector3.up));
        attackCollider.GetComponent<Attack>().Team = team;
        attackCollider.GetComponent<Attack>().Inventory = inventory;
        _lastTimeUsed = Time.time;
        _lastRandom = _weaponData.RandomDelay * Random.Range(0.0f, 1.0f);
    }

    public bool CanUse()
    {
        return Time.time - (_attackSpeed = _weaponData.GetAttackDelay(inventory)) - _lastRandom > _lastTimeUsed;
    }

    public float GetRange()
    {
        return _colliderRange;
    }
}
