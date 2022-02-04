using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public enum Team
    {
        Neutral,
        Friend,
        Foe
    }

    [SerializeField] private WeaponHandler _weaponHandler = null;
    [SerializeField] private Health _health = null;
    [SerializeField] private Team team = Team.Neutral;

    public void Attack()
    {
        _weaponHandler.Use(this.transform.position, transform.forward, team);
    }

    public bool CanAttack()
    {
        return _weaponHandler.CanUse();
    }

    private void OnTriggerEnter(Collider other)
    {
        Attack attack = other.GetComponentInParent<Attack>();
        if(attack != null && attack.Team != team)
        {
            _health.Hit(1);
        }
    }
}
