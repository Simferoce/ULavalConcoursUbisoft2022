using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnAwake : MonoBehaviour
{
    [SerializeField] private UnityEvent _onAwake = null;

    private void Start()
    {
        _onAwake?.Invoke();
    }
}
