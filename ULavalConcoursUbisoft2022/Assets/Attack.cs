using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Entity.Team Team { get; set; }
    [SerializeField] private float _speed = 0.0f;
    [SerializeField] private float _timeToLive = 0.0f;

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
