using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColissionObject : MonoBehaviour
{
    public GameObject[] panels;
    public GameObject[] itemssprite;
    public int NumberItem;
    public int currentPannel;
    public Image test;
    private Player player = null;

    // Start is called before the first frame update
    void Awake()
    {

        player = GameObject.FindObjectOfType<Player>();
        player.Entity.Inventory.OnItemReceived.AddListener(OnitemReceived);
    }

    private void OnitemReceived(ItemData item)
    {
        
        for (int i = 0; i <= NumberItem; i++)
        {
            if (!itemssprite[i].gameObject.activeInHierarchy)
            {
                itemssprite[i].gameObject.SetActive(true);
                test = itemssprite[i].GetComponent<Image>();
                test.sprite = item.Image;
            }
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
