using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private Entity.Team _team;
    public Entity.Team Team { get => _team; set => _team = value; }

    [SerializeField] private float _speed = 0.0f;
    public float Speed { get => _speed; }

    [Tooltip("-1 => infinity")]
    [SerializeField] private float _timeToLive = 0.0f;
    

    public float TimeToLive { get => _timeToLive; }

    private void Awake()
    {
        if (_timeToLive != -1)
        {
            Destroy(this.gameObject, _timeToLive);
        }
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_speed != 0 && other.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }
    }
}
