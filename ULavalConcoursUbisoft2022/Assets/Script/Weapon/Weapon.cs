using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/Weapon", order = 1)]
public class Weapon : ScriptableObject
{
    [SerializeField] private GameObject _collider = null;
    public GameObject Collider { get => _collider; }

    [SerializeField] private float _delayAttack = 0.0f;
    public float DelayAttack { get => _delayAttack; }

    [SerializeField] private float _randomDelay = 0.0f;
    public float RandomDelay { get => _randomDelay; set => _randomDelay = value; }


}
