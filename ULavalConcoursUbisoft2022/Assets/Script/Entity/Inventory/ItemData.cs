using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/ItemData", order = 1)]
public class ItemData : ScriptableObject
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private string _description = "";
    [SerializeField] private string _name = "";
    [SerializeField] private Sprite _image = null;

    public GameObject Prefab { get => _prefab; set => _prefab = value; }
    public string Description { get => _description; set => _description = value; }
    public string Name { get => _name; set => _name = value; }
    public Sprite Image { get => _image; set => _image = value; }
}
