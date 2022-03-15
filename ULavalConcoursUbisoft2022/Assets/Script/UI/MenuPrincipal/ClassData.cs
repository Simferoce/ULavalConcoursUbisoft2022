using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ClassData", menuName = "ScriptableObjects/ClassData", order = 1)]
public class ClassData : MonoBehaviour
{
    [SerializeField] private string _description = "";
    [SerializeField] private string _name = "";
    [SerializeField] private Sprite _image = null;

    public string Description { get => _description; set => _description = value; }
    public string Name { get => _name; set => _name = value; }
    public Sprite Image { get => _image; set => _image = value; }
}
