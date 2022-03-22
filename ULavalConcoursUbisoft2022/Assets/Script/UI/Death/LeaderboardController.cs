using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using UnityEngine.UI;
using TMPro;


public class LeaderboardController : MonoBehaviour
{
    public TMP_InputField MemberID;
    public Text PlayerScore;
    public int ID;
    public GameObject LeaderboardUI;
    int MaxScores = 12;
    public TextMeshProUGUI[] Entries;
    public TextMeshProUGUI[] EntriesName;
    public TextMeshProUGUI[] EntriesScore;
    public TextMeshProUGUI[] Rating;
    public int ratingint;
    private void Start()
    {
        

        LootLockerSDKManager.StartSession("Player", (response) =>
        {
            if (response.success)
            {
                Debug.Log("Success");

            }
            else
            {
                Debug.Log("Failed");
            }
        });


        if (LeaderboardUI.activeInHierarchy)
        {
            ShowScores();

        }

    }



    

    public void  ShowScores()
    {
        
        LootLockerSDKManager.GetScoreList(ID, MaxScores, (response) =>
        {
            if (response.success)
            {
                LootLocker.Requests.LootLockerLeaderboardMember[] scores = response.items;

                for(int i = 0; i< scores.Length; i++)
                {
                    Entries[i].text = scores[i].rank + "";
                    EntriesName[i].text = scores[i].member_id ;
                    EntriesScore[i].text = scores[i].score+"";
                    ratingint = int.Parse(EntriesScore[i].text);
                    if (ratingint > 2500)
                    {
                        
                        Rating[i].text = "A";
                    }
                    else if  (ratingint > 1500)
                    {
                        
                        Rating[i].text = "B";
                    }
                    else if (ratingint > 1000)
                    {

                        Rating[i].text = "C";
                    }
                    else if (ratingint > 500)
                    {

                        Rating[i].text = "D";
                    }
                    else 
                    {

                        Rating[i].text = "F";
                    }

                }
                if(scores.Length< MaxScores)
                {
                    for(int i = scores.Length; i< MaxScores; i++)
                    {
                        Entries[i].text = "";
                        EntriesName[i].text = "";
                        EntriesScore[i].text = "";
                        Rating[i].text = "";
                    }
                }
                
            }
            else
            {
                Debug.Log("Failed");
            }

        });
        
    }
public void SubmitScore()
    {
        LeaderboardUI.SetActive(true);
        LootLockerSDKManager.SubmitScore(MemberID.text,  int.Parse(PlayerScore.text), ID, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Success");

            }
            else
            {
                Debug.Log("Failed");
            }
        });
    }

  

  
}
