using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject[] _items = null;

    public GameObject[] Items { get => _items; set => _items = value; }
}
