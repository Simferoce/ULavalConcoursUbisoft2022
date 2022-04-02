using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAttack : MonoBehaviour
{
    private WeaponHandler _weaponHandler = null;


    private void Awake()
    {
        _weaponHandler = GameObject.FindObjectOfType<Player>().GetComponentInChildren<WeaponHandler>();
    }

    public void TriggerAttackFromAnimation()
    {
        _weaponHandler.TriggerAttackFromAnimation();
    }

    public void EndAttack()
    {
        _weaponHandler.EndAttack();
    }
}
