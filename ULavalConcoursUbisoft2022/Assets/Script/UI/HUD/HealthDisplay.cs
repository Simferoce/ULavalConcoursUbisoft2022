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
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
        health = player.GetComponentInChildren<Health>();
    }
   
    // Update is called once per frame
    public void HealthChange(float health)
    {
        slider.value = health;
        
    }
    
}
