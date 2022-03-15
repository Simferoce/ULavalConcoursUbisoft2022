using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    public GameObject Camera;
    
    private Player player = null;
    private Health health = null;
    public GameObject leaderboardController;
    public GameObject DeathScreentable;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
        health = player.GetComponentInChildren<Health>();
        health.OnDeath.AddListener(Health_OnDeath);
        leaderboardController.SetActive(false);
        DeathScreentable.SetActive(false);


    }

    private void Health_OnDeath(Health obj)
    {
        OnDie();
    }

    //get called when the player die
    public void OnDie()
    {
        Debug.Log("dead");       
        DeathScreentable.SetActive(true);
        Time.timeScale = 0f;
    }
    private void OnDestroy()
    {
        health.OnDeath.RemoveListener(Health_OnDeath); 
    }

    public void OnDeathBoss()
    {

        leaderboardController.SetActive(true);
        Time.timeScale = 0f;
    }
}
