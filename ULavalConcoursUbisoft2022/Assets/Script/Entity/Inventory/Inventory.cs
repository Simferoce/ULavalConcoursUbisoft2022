using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<GameObject> _items = new List<GameObject>();
    public UnityEvent<ItemData> OnItemReceived;
    public List<GameObject> Items { get => _items; set => _items = value; }


    public void AddItems(GameObject item)
    {
        _items.Add(item);

        ItemAction action;
        if(item.TryGetComponent<ItemAction>(out action))
        {
            action.Execute();
        }

        OnItemReceived?.Invoke(item.GetComponent<Item>().ItemData);

    }
}
