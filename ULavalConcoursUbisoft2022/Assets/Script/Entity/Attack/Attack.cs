using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private GameObject _owner = null;

    [SerializeField] private Transform _following = null;

    [SerializeField] private Entity.Team _team;
    public Entity.Team Team { get => _team; set => _team = value; }

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
            transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        }
    }
}
