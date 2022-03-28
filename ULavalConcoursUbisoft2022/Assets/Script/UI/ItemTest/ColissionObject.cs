using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ColissionObject : MonoBehaviour
{
    public GameObject[] panels;
    public GameObject[] itemssprite;
    public int NumberItem;
    public int currentPannel;
    public Image test;
    public GameObject[] itemdef;
    public TMP_Text itemdefinition;
    
    private Player player = null;

    // Start is called before the first frame update
    void Awake()
    {

        player = GameObject.FindObjectOfType<Player>();
        player.Entity.Inventory.OnItemReceived.AddListener(OnitemReceived);
    }

    public void OnitemReceived(ItemData item)
    {
        NumberItem += 1;
        for (int i = 0; i <= NumberItem; i++)
        {
            panels[i].gameObject.SetActive(true);
            if (item.Image == test.sprite && itemssprite[i].gameObject.activeInHierarchy)
            {
                Debug.Log("same item");
                NumberItem -= 1;
                return;

            }
            else if (!itemssprite[i].gameObject.activeInHierarchy)
            {
                
                itemssprite[i].gameObject.SetActive(true);
                test = itemssprite[i].GetComponent<Image>();
                test.sprite = item.Image;
                itemdefinition = itemdef[i].GetComponent<TMP_Text>();
                itemdefinition.text = item.Description;
                



            }
             
            else
            {
                Debug.Log("different items");
            }



        }
       

    }

    



    public void NewObject()
    {
        Debug.Log("wtf why");
        NumberItem += 1;
        for (int i = 0; i < NumberItem; i++)
        {
            panels[i].gameObject.SetActive(true);
        }


    }
    
}
