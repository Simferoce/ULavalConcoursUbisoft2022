using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/Weapon", order = 1)]
public class Weapon : ScriptableObject
{
    public enum WeaponType
    {
        Melee,
        Ranged
    }

    [SerializeField] private GameObject _collider = null;
    [SerializeField] private GameObject _weaponModel = null;
    [SerializeField] private AttackSpeedAttribute attackSpeedAttribute = null;
    [SerializeField] private WeaponType _type = WeaponType.Melee;
    [SerializeField] private GameObject _slash = null;

    public WeaponType Type { get => _type; set => _type = value; }
    public GameObject WeaponModel { get => _weaponModel; set => _weaponModel = value; }
    public GameObject Collider { get => _collider; }
    public GameObject Slash { get => _slash; set => _slash = value; }

    public float GetAttackDelay(Inventory inventory)
    {
        return 1 / attackSpeedAttribute.GetValue(inventory);
    }
}
