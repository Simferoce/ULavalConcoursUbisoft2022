using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Onhover : MonoBehaviour
{
    public GameObject panel;
public void OnHover()
    {
        
       panel.SetActive(true);

    }

public void OnExit()
    {
        panel.SetActive(false);

    }
}
