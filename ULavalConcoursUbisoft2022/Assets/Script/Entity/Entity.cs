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

    public float AttackRange()
    {
        return _weaponHandler.GetRange();
    }

    private void OnTriggerEnter(Collider other)
    {
        Attack attack = other.GetComponentInParent<Attack>();
        if(attack != null && attack.Team != team)
        {
            _health.Hit(1);
        }
    }

    public bool Sees(Vector3 position)
    {
        Vector3 seeingPositionOnAPlane = Vector3.ProjectOnPlane(position, Vector3.up);
        Vector3 positionOnAPlane = Vector3.ProjectOnPlane(transform.position, Vector3.up);

        RaycastHit hit;
        return !Physics.SphereCast(transform.position, 0.1f, seeingPositionOnAPlane - positionOnAPlane, out hit, (seeingPositionOnAPlane - positionOnAPlane).magnitude, LayerMask.GetMask("Wall"));
    }
}
