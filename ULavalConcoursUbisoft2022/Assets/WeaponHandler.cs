using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private Weapon _weaponData = null;
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
}
