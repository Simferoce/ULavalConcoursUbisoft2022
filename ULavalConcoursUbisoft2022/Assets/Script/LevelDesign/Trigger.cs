using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    public UnityEvent actions = null;
    [SerializeField] private bool _destroyOnTrigger = true; 

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            actions?.Invoke();
            if(_destroyOnTrigger)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
