using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController = null;
    [SerializeField] private float _speed = 0.0f;
    [SerializeField] private Aim _aim = null;

    private Vector3 direction = Vector3.zero;

    private void Awake()
    {
        
    }

    private void Update()
    {
        direction = new Vector3(Input.GetAxisRaw("Horizontal"),0, Input.GetAxisRaw("Vertical"));
    }

    private void FixedUpdate()
    {
        _characterController.Move(direction * _speed);
        Vector3 positionToLookAt = new Vector3(_aim.transform.position.x, this.transform.position.y, _aim.transform.position.z);
        transform.LookAt(positionToLookAt, Vector3.up);
    }
}
