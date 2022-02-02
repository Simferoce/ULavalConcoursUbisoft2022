using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/Weapon", order = 1)]
public class Weapon : ScriptableObject
{
    public GameObject _collider = null;
    public float _timeToLive = 0.0f;

    public void Attack(Vector3 origin, Vector3 direction, Entity.Team team)
    {
        GameObject attackCollider = Instantiate(_collider, origin, Quaternion.LookRotation(direction, Vector3.up));
        attackCollider.GetComponent<Attack>().Team = team;
        Destroy(attackCollider, _timeToLive);
    }
}
