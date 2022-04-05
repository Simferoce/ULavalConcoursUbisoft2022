using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    public UnityEvent actions = null;
    [SerializeField] private bool _destroyOnTrigger = true;
    [SerializeField] private bool _once = true;

    private bool _hasBeenTrigger = false;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && ((_once && !_hasBeenTrigger) || !_once))
        {
            actions?.Invoke();
            if(_destroyOnTrigger && _once)
            {
                Destroy(this.gameObject);
            }

            _hasBeenTrigger = true;
        }
    }
}
