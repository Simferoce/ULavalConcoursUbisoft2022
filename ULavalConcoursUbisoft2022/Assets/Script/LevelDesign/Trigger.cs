using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    public UnityEvent actions = null;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            actions?.Invoke();
            Destroy(this.gameObject);
        }
    }
}
