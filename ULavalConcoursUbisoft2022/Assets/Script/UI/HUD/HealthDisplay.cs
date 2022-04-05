using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    private Player player = null;
    public  Health health = null;
    public GameObject barr;
    public float maxHealth;
    public Slider slider;
    public float currentHealth;
    public Image barrimage;

    private Color startColor = Color.white;
    public Color endColor = Color.green;
    [Range(0, 10)]
    public float speed = 15;

    

    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
        health = player.GetComponentInChildren<Health>();
        barrimage = barr.GetComponent<Image>();
        startColor = barrimage.color;
        currentHealth = health.MaxHealth;
        slider.value = 0.0f;
        maxHealth = health.MaxHealth;
        slider.maxValue = maxHealth;
        
    }

    public void Update()
    {
        
        currentHealth = health.HealthPoint;
        slider.value = maxHealth - currentHealth;
        barrimage.color = Color.Lerp(startColor, endColor, Mathf.PingPong(Time.time * speed, 1));
    }
    public void healthbareffect()
    {
        barrimage.color = Color.Lerp(startColor, endColor, Mathf.PingPong(Time.time * speed, 1));
    }
}
