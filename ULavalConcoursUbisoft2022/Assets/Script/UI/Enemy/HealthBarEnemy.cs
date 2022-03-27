using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarEnemy : MonoBehaviour
{
    [SerializeField] private Image _healthbarSprite = null;
    [SerializeField] private Health _health = null;
    [SerializeField] private Image _comboBar = null;
    [SerializeField] private float _comboTime = 0.0f;
    [SerializeField] private float _speed = 0.0f;

    private Camera _cam;
    private float _lastTimeLostHealth = 0.0f;

    private bool _drainningComboBar = false;
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

        if (Time.time < _lastTimeLostHealth + _comboTime)
        {
            if(_drainningComboBar)
            {
                _comboBar.fillAmount = _healthbarSprite.fillAmount;
                _drainningComboBar = false;
            }
            _healthbarSprite.fillAmount = _health.HealthPoint / _health.MaxHealth;
        }
        else
        {
            if (_comboBar.fillAmount != _health.HealthPoint / _health.MaxHealth)
            {
                _drainningComboBar = true;
                _comboBar.fillAmount -= _speed * Time.deltaTime;

                if (_comboBar.fillAmount < _health.HealthPoint / _health.MaxHealth)
                {
                    _comboBar.fillAmount = _health.HealthPoint / _health.MaxHealth;
                }
            }
            else
            {
                _drainningComboBar = false;
            }
        }
    }

    public void UpdateHealth(Health health, float damage)
    {
        _lastTimeLostHealth = Time.time;
    }

    private void OnDestroy()
    {
        _health.OnDamage -= UpdateHealth;
    }
}
