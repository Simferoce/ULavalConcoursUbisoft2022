using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ClassData", menuName = "ScriptableObjects/ClassData", order = 1)]
public class ClassData : ScriptableObject
{
    [SerializeField] private string _name = "";
    [SerializeField] private string _description = "";
    [SerializeField] private string _statisticDetail = "";
    [SerializeField] private Sprite _icon = null;
    [SerializeField] private Weapon _weapon = null;
    [SerializeField] private GameObject _modelPrefab = null;
    [SerializeField] private ItemData _startingItem = null;

    public string Name { get => _name; set => _name = value; }
    public string Description { get => _description; set => _description = value; }
    public string StatisticDetail { get => _statisticDetail; set => _statisticDetail = value; }
    public GameObject ModelPrefab { get => _modelPrefab; set => _modelPrefab = value; }
    public Sprite Icon { get => _icon; set => _icon = value; }
    public Weapon Weapon { get => _weapon; set => _weapon = value; }
    public ItemData StartingItem { get => _startingItem; set => _startingItem = value;}
}
