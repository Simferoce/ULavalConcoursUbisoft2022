using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStopOnTrigger : Attack
{
    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}
