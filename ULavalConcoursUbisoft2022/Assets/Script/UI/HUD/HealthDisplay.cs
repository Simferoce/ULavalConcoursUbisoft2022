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

    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
        health = player.GetComponentInChildren<Health>();
        currentHealth = health.MaxHealth;
        slider.value = 0.0f;
        maxHealth = health.MaxHealth;
        slider.maxValue = maxHealth;
       
    }

    public void Update()
    {
        currentHealth = health.HealthPoint;
        slider.value = maxHealth - currentHealth;
    }
}
