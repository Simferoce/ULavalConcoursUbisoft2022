using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBubbleMessage : MonoBehaviour
{
    public void ShowMessage(int id)
    {
        GameObject.FindObjectOfType<Player>().GetComponentInChildren<BubbleText>().ShowMessage(id);
    }

    public void TriggerEndMessage(int id)
    {
        GameObject.FindObjectOfType<Player>().GetComponentInChildren<BubbleText>().TriggerEndMessage(id);
    }
}
