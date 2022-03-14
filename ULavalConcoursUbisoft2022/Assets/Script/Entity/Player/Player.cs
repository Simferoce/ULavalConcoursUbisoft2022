using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController = null;
    [SerializeField] private MovespeedAttribute _movespeedAttribute = null;
    [SerializeField] private Transform _aim = null;
    [SerializeField] private float _speed = 0.0f;
    
    [SerializeField] private Entity _entity = null;

    private Vector3 direction = Vector3.zero;
    private bool _lock = false;

    public Entity Entity { get => _entity; set => _entity = value; }
    public bool Lock { get => _lock; set => _lock = value; }

    private void Awake()
    {
        
    }

    private void Update()
    {
        if (!_lock)
        {
            direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

            if (Input.GetButtonDown("Fire1"))
            {
                _entity.Attack();
            }

            _characterController.Move(direction * _movespeedAttribute.GetValue(_entity.Inventory) * Time.deltaTime);
            _entity.Translation = direction;
            Vector3 positionToLookAt = new Vector3(_aim.transform.position.x, this.transform.position.y, _aim.transform.position.z);
            transform.LookAt(positionToLookAt, Vector3.up);
        }
    }

    public void LockControl()
    {
        _lock = true;
    }

    public void UnlockControl()
    {
        _lock = false;
    }
}
