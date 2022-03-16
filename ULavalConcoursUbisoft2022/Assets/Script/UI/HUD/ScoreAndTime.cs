using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreAndTime : MonoBehaviour
{
    public Text timeCounter;
    public Text ScoreCounter;
    private float timer = 0.0f;
    
    private float score = 0f;
    private void Start()
    {
        timeCounter.text = "00:00";
        ScoreCounter.text = "0";
        
    }
    
    void Update()
    {
        if (MenuPause.GameIsPaused == false)
        {
            timer += Time.deltaTime;
            DisplayTime();
            
        }
    }
    void DisplayTime()
    {
        int minutes = Mathf.FloorToInt(timer / 60.0f);
        int seconds = Mathf.FloorToInt(timer - minutes * 60);
        timeCounter.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void AddpointMinions()
    {
        Debug.Log("reee");
        score += 5;
        ScoreCounter.text = string.Format("{0000} ", score);
    }
}
