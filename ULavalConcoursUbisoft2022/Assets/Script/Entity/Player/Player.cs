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
    [SerializeField] private Inventory _inventory = null;

    private Vector3 direction = Vector3.zero;
    private bool _lock = false;
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

            _characterController.Move(direction * _movespeedAttribute.GetValue(_inventory) * Time.deltaTime);
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
