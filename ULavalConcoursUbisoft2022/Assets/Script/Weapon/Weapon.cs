using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/Weapon", order = 1)]
public class Weapon : ScriptableObject
{
    [SerializeField] private GameObject _collider = null;
    public GameObject Collider { get => _collider; }

    [SerializeField] private AttackSpeedAttribute attackSpeedAttribute = null;

    public enum WeaponType
    {
        Melee,
        Ranged
    }

    [SerializeField] private WeaponType _type = WeaponType.Melee;
    public WeaponType Type { get => _type; set => _type = value; }


    public float GetAttackDelay(Inventory inventory)
    {
        return 1 / attackSpeedAttribute.GetValue(inventory);
    }
}
