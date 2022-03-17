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
        
        score += 5;
        ScoreCounter.text = string.Format("{0000} ", score);
    }
    public void AddpointFinalBoss()
    {

        score += 1000;
        ScoreCounter.text = string.Format("{0000} ", score);
    }
    public void WrathKill()
    {

        score += 100;
        ScoreCounter.text = string.Format("{0000} ", score);
    }

    public void BossWrathKill()
    {

        score += 500;
        ScoreCounter.text = string.Format("{0000} ", score);
    }

    public void KillExhaustion()
    {

        score += 100;
        ScoreCounter.text = string.Format("{0000} ", score);
    }

    public void KillExhaustionBoss()
    {

        score += 500;
        ScoreCounter.text = string.Format("{0000} ", score);
    }

    public void SadnessKill()
    {

        score += 50;
        ScoreCounter.text = string.Format("{0000} ", score);
    }

    public void SadnessKillBoss()
    {

        score += 500;
        ScoreCounter.text = string.Format("{0000} ", score);
    }
}

