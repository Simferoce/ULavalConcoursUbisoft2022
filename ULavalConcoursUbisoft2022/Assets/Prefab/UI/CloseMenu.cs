using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CloseMenu : MonoBehaviour
{
    [SerializeField] private UnityEvent _events = null;

    public void CloseMenuAction()
    {
        _events?.Invoke();
    }
}
