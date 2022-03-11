using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LootLocker.Requests;


public class LeaderboardController : MonoBehaviour
{
    public InputField PlayerName, PlayerScore;
    public int ID;

    private void Start()
    {
        LootLockerSDKManager.StartSession("Player", (response) =>
         {
             if (response.success)
             {
                 Debug.Log("success");

             }
             else
             {
                 Debug.Log("failed");

             }

         });
    }

    public void SubmitScore()
        {
            LootLockerSDKManager.submiScore(PlayerName.text, int.Parse(PlayerScore.text), ID, (response) =>
            {
                if (response.success)
                {
                    Debug.Log("success");

                }
                else
                {
                    Debug.Log("failed");

                }


            });

        }
    
}

