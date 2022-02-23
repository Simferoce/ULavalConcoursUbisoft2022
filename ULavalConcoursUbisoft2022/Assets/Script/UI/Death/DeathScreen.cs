using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    public GameObject Camera;
    public GameObject DeathMessage;
    private Player player = null;
    private Health health = null;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
        health = player.GetComponentInChildren<Health>();
        health.OnDeath += Health_OnDeath; 
        DeathMessage.SetActive(false);
        
        
    }

    private void Health_OnDeath(Health obj)
    {
        OnDie();
    }

    //get called when the player die
    public void OnDie()
    {
        DeathMessage.SetActive(true);
        Camera.transform.position = Camera.transform.position + new Vector3(30, 30, 30);
    }
    private void OnDestroy()
    {
        health.OnDeath -= Health_OnDeath; 
    }
}
