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
    [SerializeField] private PushBackHandler _pushBackHandler = null;
    [SerializeField] private Health _health = null;
    [SerializeField] private Team team = Team.Neutral;
    [SerializeField] private Transform root = null;

    [SerializeField] private CharacterController _characterController = null;

    public PushBackHandler PushBackHandler { get => _pushBackHandler; set => _pushBackHandler = value; }
    public Health Health { get => _health; set => _health = value; }

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
        if(attack != null && attack.Owner != this.gameObject && attack.Team != team)
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

    public void LookTowardsTarget(Vector3 target)
    {
        root.LookAt(new Vector3(target.x, transform.position.y, target.z));
    }

    public void Move(Vector3 translation)
    {
        if (_characterController)
        {
            _characterController.Move(translation);
            Physics.SyncTransforms();
        }
        else
        {
            root.Translate(translation, Space.World);
            Physics.SyncTransforms();
        }
    }
}
