using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private GameObject _owner = null;

    [SerializeField] private Transform _following = null;

    [SerializeField] private Entity.Team _team;
    public Entity.Team Team { get => _team; set => _team = value; }

    [SerializeField] private Inventory _inventory;
    public Inventory Inventory { get => _inventory; set => _inventory = value; }

    [SerializeField] private AttackDamageAttribute attackDamageAttribute = null;

    [SerializeField] private float _speed = 0.0f;
    public float Speed { get => _speed; }
    public Transform Following { get => _following; set => _following = value; }
    public GameObject Owner { get => _owner; set => _owner = value; }


    private void Update()
    {
        if (_following != null)
        {
            this.transform.position = _following.position;
            this.transform.rotation = _following.rotation;
        }
        else
        {
            Vector3 forwardOnPlane = Vector3.ProjectOnPlane(transform.forward, Vector3.up);
            transform.position += forwardOnPlane * _speed * Time.deltaTime;
        }
    }

    public float GetAttackDamage()
    {
        return attackDamageAttribute.GetValue(Inventory);
    }
}
