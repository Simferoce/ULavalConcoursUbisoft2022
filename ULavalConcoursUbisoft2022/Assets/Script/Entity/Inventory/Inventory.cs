using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject[] items;

    GameObject[] getInventory()
    {
        return items;
    }
    
}
