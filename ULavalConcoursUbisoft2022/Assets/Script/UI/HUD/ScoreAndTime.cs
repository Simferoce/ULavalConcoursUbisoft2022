using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class ScoreAndTime : MonoBehaviour
{
    public Text timeCounter;
    public Text ScoreCounter;
    public Text PlayerScore;
    public Health health = null;
    public TMP_Text PlayerScoreDisplay;
    public TMP_Text PlayerMark;
    public TMP_Text HealthRemaining;
    public TMP_Text HealthScore;
    public int HealthRemainingint;
    public int ratingint;
    public GameObject LeaderboardUI;
    private Player player = null;
    private float timer = 0.0f;
    private float score = 0f;

    private void Start()
    {
        timeCounter.text = "00:00";
        ScoreCounter.text = "0";
        player = GameObject.FindObjectOfType<Player>();
        health = player.GetComponentInChildren<Health>();


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

    public void ShowScore()
    {
        PlayerScoreDisplay.text = PlayerScore.text + " pts";

    }
    public void ShowMark()
    {
        Debug.Log("showmark working");
        ratingint = int.Parse(PlayerScore.text);
        if (ratingint > 2500)
        {

            PlayerMark.text = "A";
        }
        else if (ratingint > 1500)
        {

            PlayerMark.text = "B";
        }
        else if (ratingint > 1000)
        {

            PlayerMark.text = "C";
        }
        else if (ratingint > 500)
        {

            PlayerMark.text = "D";
        }
        else
        {

            PlayerMark.text = "F";
        }
    }
    public void BonusPoint()
    {
       Debug.Log("working bonus points");
       HealthRemainingint = (int)health.HealthPoint;
        score += HealthRemainingint;
        ScoreCounter.text = string.Format("{0000} ", score);
        HealthRemaining.text =  HealthRemainingint.ToString();
        HealthScore.text = "+" + HealthRemaining.text;
    }


}

