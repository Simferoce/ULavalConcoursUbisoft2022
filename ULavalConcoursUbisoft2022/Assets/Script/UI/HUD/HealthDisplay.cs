using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    private Player player = null;
    public  Health health = null;
    public float maxHealth;
    public Slider slider;
    public float currentHealth;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
        health = player.GetComponentInChildren<Health>();
        health.OnDamage += Health_OnDamage;
        slider.value = 0;
    }

    private void Health_OnDamage(Health arg1, float arg2)
    {
        currentHealth = health.HealthPoint;
        slider.value = maxHealth-currentHealth;
    }

    private void OnDestroy()
    {
        health.OnDamage -= Health_OnDamage;
    }


}
