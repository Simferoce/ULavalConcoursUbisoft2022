using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColissionObject : MonoBehaviour
{
    public GameObject[] panels;
    public int NumberItem;
    public int currentPannel;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

 

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name == "Body")
        {

            //L<object va être mis en argument
            NewObject();

        }
    }
    public void NewObject()
    {
        NumberItem += 1;
        for (int i = 0; i < NumberItem; i++)
        {
            panels[i].gameObject.SetActive(true);

            if (panels[i].gameObject.transform.childCount == 0)
            {
                

            }
           

        }
        

    }

}
