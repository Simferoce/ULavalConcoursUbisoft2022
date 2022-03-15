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
    [SerializeField] private GameObject _colliders = null;
    [SerializeField] private GameObject _visual = null;
    [SerializeField] private Inventory _inventory = null;
    [SerializeField] private DamageReductionAttribute _damageReductionAttribute = null;

    [SerializeField] private CharacterController _characterController = null;

    public PushBackHandler PushBackHandler { get => _pushBackHandler; set => _pushBackHandler = value; }
    public Health Health { get => _health; set => _health = value; }

    [SerializeField] private Vector3 _translation = Vector3.zero;

    public Vector3 Translation { set { _translation = value; } get { return _translation; } }

    public bool IsMoving { get { return Vector3.Distance(Translation, Vector3.zero) > 0.01f; } }

    public Transform Root { get => root; set => root = value; }
    public WeaponHandler WeaponHandler { get => _weaponHandler; set => _weaponHandler = value; }
    public Inventory Inventory { get => _inventory; set => _inventory = value; }

    public event System.Action<Weapon.WeaponType> OnAttack;

    private void Update()
    {
        if (_characterController != null)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.parent.position, Vector3.down, out hit, 100, LayerMask.GetMask("Ground")))
            {
                transform.parent.position = new Vector3(transform.parent.position.x, hit.point.y + _characterController.height / 2, transform.position.z);
                Physics.SyncTransforms();
            }
        }
    }

    public void Attack()
    {
        if (CanAttack())
        {
            _weaponHandler.Use(this.transform.position, transform.forward, team, _inventory);
            OnAttack?.Invoke(_weaponHandler.WeaponData.Type);
        }
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
            _health.Hit(attack.GetAttackDamage() * (2 - _damageReductionAttribute.GetValue(_inventory)), attack.Team);
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

    public void Teleport(Vector3 position)
    {
        root.transform.position = position;
        Physics.SyncTransforms();
    }

    public void Teleport(Vector3 position, Vector3 forward)
    {
        root.rotation = Quaternion.Euler(forward);
        root.transform.position = position;
        Physics.SyncTransforms();
    }

    public void Vanish()
    {
        GetComponentInChildren<HealthBarEnemy>(true)?.gameObject?.SetActive(false);
        _colliders.SetActive(false);
        _visual.SetActive(false);
    }

    public void Appear()
    {
        GetComponentInChildren<HealthBarEnemy>(true)?.gameObject?.SetActive(true);
        _colliders.SetActive(true);
        _visual.SetActive(true);
    }
}
