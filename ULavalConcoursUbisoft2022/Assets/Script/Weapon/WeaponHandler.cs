using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private Weapon _startingWeapon = null;
    [SerializeField] private Inventory _inventory = null;

    private Weapon _weaponData = null;
    private Attack _attack = null;
    private float _colliderRange = 0.0f;

    private float _lastTimeUsed = 0.0f;
    private System.Action _triggerAttack;

    public Weapon WeaponData { get => _weaponData; set => _weaponData = value; }

    private bool _isAttacking = false;
    public bool IsAttacking { get => _isAttacking; set => _isAttacking = value; }

    private Transform _weaponAnchor = null;
    private VisualEffect _slash = null;

    public UnityEvent OnAttack;

    private List<GameObject> _attackObjects = new List<GameObject>();

    private void Start()
    {
        if(_startingWeapon != null)
        {
            SetWeapon(_startingWeapon);
        }
    }

    public void SetWeapon(Weapon weapon)
    {
        if(weapon.WeaponModel != null)
        {
            _weaponAnchor = this.transform.parent.GetComponentInChildren<WeaponAnchor>().transform;
            GameObject weaponObject = Instantiate(weapon.WeaponModel, _weaponAnchor);
            _slash = Instantiate(weapon.Slash, this.transform.parent).GetComponentInChildren<VisualEffect>();
        }

        _weaponData = weapon;
        _attack = weapon.Collider.GetComponent<Attack>();
        
        _lastTimeUsed = Time.time - _weaponData.GetAttackDelay(_inventory);

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

    public void TriggerAttackFromAnimation()
    {
        _triggerAttack();
    }

    public void Use(Transform source, Entity.Team team, Inventory inventory, bool waitAnimation = false)
    {
        _isAttacking = true;

        _triggerAttack = () =>
        {
            Vector3 spawnPosition = _weaponAnchor != null ? _weaponAnchor.position : source.position;
            GameObject attackCollider = Instantiate(_weaponData.Collider, spawnPosition, Quaternion.LookRotation(source.forward, Vector3.up));
            attackCollider.GetComponent<Attack>().Team = team;
            attackCollider.GetComponent<Attack>().Inventory = inventory;

            _attackObjects.Add(attackCollider);
            _slash?.Play();
            OnAttack?.Invoke();
        };

        if(!waitAnimation)
        {
            _triggerAttack();
        }
        _lastTimeUsed = Time.time;
    }

    public bool CanUse()
    {
        return Time.time - (_weaponData.GetAttackDelay(_inventory)) > _lastTimeUsed;
    }

    public float GetRange()
    {
        return _colliderRange;
    }

    public void EndAttack()
    {
        _isAttacking = false;
    }

    private void OnDestroy()
    {
        foreach(GameObject obj in _attackObjects)
        {
            if(obj != null)
            {
                Destroy(obj);
            }
        }
    }
}
