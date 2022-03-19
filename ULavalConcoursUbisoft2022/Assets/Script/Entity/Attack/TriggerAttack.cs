using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAttack : MonoBehaviour
{
    public void TriggerAttackFromAnimation()
    {
        transform.parent.GetComponentInChildren<WeaponHandler>().TriggerAttackFromAnimation();
    }
}
