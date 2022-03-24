using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineX : MonoBehaviour
{
    [SerializeField] private State EntryPoint = null;

    private void Start()
    {
        EntryPoint.enabled = true;
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}
