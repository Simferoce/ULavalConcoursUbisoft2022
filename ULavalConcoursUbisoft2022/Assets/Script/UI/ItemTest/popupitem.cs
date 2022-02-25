using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popupitem : MonoBehaviour
{
    public GameObject popup;
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name == "Body")
        {

            //L'object va être mis en argument
            PopUp();

        }
    }

    public void PopUp()
    {
        popup.SetActive(true);
        Time.timeScale = 0f;
        

    }
    public void OnSelection()
    {
        popup.SetActive(false);
        Time.timeScale = 1f;
    }
   
}
