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
        timeCounter.text = "Time :00:00";
        ScoreCounter.text = "Score : 0";
        InvokeRepeating("Addpoint", 5.0f,5.0f);
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
        timeCounter.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);
    }

    void Addpoint()
    {
        
        score += 5;
        ScoreCounter.text = string.Format("Score: {0000} ", score);
    }
}
