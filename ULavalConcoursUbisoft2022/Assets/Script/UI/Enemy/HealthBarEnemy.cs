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
        transform.rotation = Quaternion.Euler(Vector3.Scale((Quaternion.Inverse(_cam.transform.rotation)).eulerAngles, new Vector3(0, 1, 1)));
    }

    public void Update()
    {
        transform.rotation = Quaternion.Euler(Vector3.Scale((_cam.transform.rotation).eulerAngles, new Vector3(1, 0, 0)));

        //transform.rotation = Quaternion.Euler(Vector3.Scale(Quaternion.LookRotation(transform.position - _cam.transform.position).eulerAngles, new Vector3(1, 1, 0)));
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
