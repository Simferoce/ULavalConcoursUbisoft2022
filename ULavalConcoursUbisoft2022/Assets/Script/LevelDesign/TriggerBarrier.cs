using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerBarrier : MonoBehaviour
{
    [SerializeField] private int _number = 0;

    [SerializeField] private UnityEvent _event = null;

    private int _currentNumber = 0;
    public void Invoke()
    {
        if(++_currentNumber == _number)
        {
            _event?.Invoke();
        }
    }
}
