using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private bool _destroyOnHit = true;
    [SerializeField] private bool _destroyOnHitWall = true;
    [SerializeField] private GameObject _owner = null;
    [SerializeField] private Transform _following = null;
    [SerializeField] private Entity.Team _team;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private AttackDamageAttribute attackDamageAttribute = null;
    [SerializeField] private float _speed = 0.0f;
    [SerializeField] private bool _spin = false;
    [SerializeField] private GameObject _visual = null;

    public float Speed { get => _speed; }
    public Transform Following { get => _following; set => _following = value; }
    public GameObject Owner { get => _owner; set => _owner = value; }
    public bool DestroyOnHit { get => _destroyOnHit; set => _destroyOnHit = value; }
    public Entity.Team Team { get => _team; set => _team = value; }
    public Inventory Inventory { get => _inventory; set => _inventory = value; }

    private Vector3 _randomSpin = Vector3.zero;
    private void Awake()
    {
        _randomSpin = Random.insideUnitSphere;
    }

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

            if(_spin)
            {
                _visual.transform.Rotate(_randomSpin);
            }
        }
    }

    public float GetAttackDamage()
    {
        return attackDamageAttribute.GetValue(Inventory);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(_destroyOnHitWall && other.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }
    }
}
