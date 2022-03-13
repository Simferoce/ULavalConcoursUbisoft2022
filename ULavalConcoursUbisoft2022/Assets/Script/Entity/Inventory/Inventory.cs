using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<GameObject> _items = new List<GameObject>();

    public List<GameObject> Items { get => _items; set => _items = value; }
}
