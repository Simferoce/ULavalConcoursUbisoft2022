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

    [SerializeField] private Weapon _weapon = null;
    [SerializeField] private Health _health = null;
    [SerializeField] private Team team = Team.Neutral;

    public void Attack()
    {
        _weapon.Attack(this.transform.position, transform.forward, team);
    }

    private void OnTriggerEnter(Collider other)
    {
        Attack attack = other.GetComponentInParent<Attack>();
        if(attack != null)
        {
            _health.Hit(1);
        }
    }
}
