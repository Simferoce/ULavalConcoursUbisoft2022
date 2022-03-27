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

    public TMP_Text Sadnesskill;
    public TMP_Text SadnesskillScore;

    public TMP_Text Wrathkill;
    public TMP_Text WrathkillScore;

    public TMP_Text Exhaustion;
    public TMP_Text ExhaustionScore;

    public TMP_Text FinalBoss;
    public TMP_Text FinalBossScore;

    public TMP_Text SadnesskillMinion;
    public TMP_Text SadnesskillMinionScore;

    public TMP_Text WrathkillMinion;
    public TMP_Text WrathkillMinionScore;

    public TMP_Text ExhaustionMinion;
    public TMP_Text ExhaustionMinionScore;

    public TMP_Text TotalScore;

    public int HealthRemainingint;
    public int ratingint;
    public GameObject LeaderboardUI;
    public TMP_Text LevelStatut;

    private Player player = null;
    private float timer = 0.0f;
    private float score = 0f;

    private void Start()
    {
        timeCounter.text = "00:00";
        ScoreCounter.text = "0";
        player = GameObject.FindObjectOfType<Player>();
        health = player.GetComponentInChildren<Health>();
        
        Sadnesskill.text = "x0";
        SadnesskillScore.text = "+0";
        
        
        Wrathkill.text = "x0";
        WrathkillScore.text = "+0";
        
        Exhaustion.text = "x0";
        ExhaustionScore.text = "+0";

        FinalBoss.text = "x0";
        FinalBossScore.text = "+0";

        ExhaustionMinion.text = "x0";
        ExhaustionMinionScore.text = "+0";

        WrathkillMinion.text = "x0";
        WrathkillMinionScore.text = "+0";

        SadnesskillMinion.text = "x0";
        SadnesskillMinionScore.text = "+0";

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
        FinalBoss.text = "x1";
        FinalBossScore.text = "+1000";
        LevelStatut.text = "Success";
    }
    public void WrathKill()
    {

        score += 100;
        ScoreCounter.text = string.Format("{0000} ", score);
        WrathkillMinion.text = "x1";
        WrathkillMinionScore.text = "+100";

    }

    public void BossWrathKill()
    {

        score += 500;
        ScoreCounter.text = string.Format("{0000} ", score);
        Wrathkill.text = "x1";
        WrathkillScore.text = "+500";
    }

    public void KillExhaustion()
    {

        score += 100;
        ScoreCounter.text = string.Format("{0000} ", score);
        ExhaustionMinion.text = "x1";
        ExhaustionMinionScore.text = "+100";

    }

    public void KillExhaustionBoss()
    {

        score += 500;
        ScoreCounter.text = string.Format("{0000} ", score);
        Exhaustion.text = "x1";
        ExhaustionScore.text = "+500";
    }

    public void SadnessKill()
    {

        score += 50;
        ScoreCounter.text = string.Format("{0000} ", score);
         SadnesskillMinion.text = "x2";
     SadnesskillMinionScore.text = "+50";
}

    public void SadnessKillBoss()
    {

        score += 500;
        ScoreCounter.text = string.Format("{0000} ", score);
        Sadnesskill.text = "x1";
        SadnesskillScore.text = "+500";
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
       
       HealthRemainingint = (int)health.HealthPoint;
        score += HealthRemainingint;
        ScoreCounter.text = string.Format("{0000} ", score);
        HealthRemaining.text =  HealthRemainingint.ToString();
        HealthScore.text = "+" + HealthRemaining.text;

        TotalScore.text = PlayerScore.text + "pts";
    }


}

