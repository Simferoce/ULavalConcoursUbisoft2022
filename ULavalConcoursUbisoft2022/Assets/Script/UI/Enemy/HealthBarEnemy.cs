using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarEnemy : MonoBehaviour
{
    [SerializeField] private Image _healthbarSprite;
    [SerializeField] private Health _health = null;
    private Camera _cam;


    private void Awake()
    {
        _health.OnDamage += UpdateHealth;
    }

    private void Start()
    {
        _cam = Camera.main;
    }

    public void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - _cam.transform.position);
    }

    public void UpdateHealth(Health health, float damage)
    {
        _healthbarSprite.fillAmount = health.HealthPoint / health.MaxHealth;
    }

    private void OnDestroy()
    {
        _health.OnDamage -= UpdateHealth;
    }
}
