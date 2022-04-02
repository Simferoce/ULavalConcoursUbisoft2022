using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MaxActionPerSeconds : MonoBehaviour
{
    [SerializeField] private float _maxCallPerSeconds = 0.0f;
    [SerializeField] private UnityEvent _action = null;

    private float _lastCall = 0.0f;

    public void TryExecute()
    {
        if(Time.time > _lastCall + _maxCallPerSeconds)
        {
            _action?.Invoke();
            _lastCall = Time.time;
        }
    }
}
