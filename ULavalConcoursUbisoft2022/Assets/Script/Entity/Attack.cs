using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Entity.Team Team { get; set; }

    [SerializeField] private float _speed = 0.0f;
    public float Speed { get => _speed; }

    [SerializeField] private float _timeToLive = 0.0f;
    public float TimeToLive { get => _timeToLive; }

    private void Awake()
    {
        Destroy(this.gameObject, _timeToLive);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }
    }
}
