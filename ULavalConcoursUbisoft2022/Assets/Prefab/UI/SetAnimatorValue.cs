using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAnimatorValue : MonoBehaviour
{
    public void SetTrue(string name)
    {
        GetComponent<Animator>().SetBool(name, true);
    }

    public void SetFalse(string name)
    {
        GetComponent<Animator>().SetBool(name, false);
    }
}
