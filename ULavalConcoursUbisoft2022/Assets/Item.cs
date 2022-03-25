using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemData _itemData = null;

    public ItemData ItemData { get => _itemData; set => _itemData = value; }
}
