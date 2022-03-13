using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController = null;
    [SerializeField] private MovespeedAttribute _movespeedAttribute = null;
    [SerializeField] private Aim _aim = null;
    [SerializeField] private Entity _entity = null;
    [SerializeField] private Inventory _inventory = null;

    private Vector3 direction = Vector3.zero;

    private void Awake()
    {
        
    }

    private void Update()
    {
        direction = new Vector3(Input.GetAxisRaw("Horizontal"),0, Input.GetAxisRaw("Vertical"));

        if (Input.GetButtonDown("Fire1"))
        {
            _entity.Attack();
        }

        _characterController.Move(direction * _movespeedAttribute.GetValue(_inventory) * Time.deltaTime);
        Vector3 positionToLookAt = new Vector3(_aim.transform.position.x, this.transform.position.y, _aim.transform.position.z);
        transform.LookAt(positionToLookAt, Vector3.up);
    }
}
