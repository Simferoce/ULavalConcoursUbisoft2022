using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CloseMenu : MonoBehaviour
{
    [SerializeField] private UnityEvent _openEvents = null;
    [SerializeField] private UnityEvent _closeEvents = null;

    public void CloseMenuAction()
    {
        _closeEvents?.Invoke();
    }

    public void OpenMenuAction()
    {
        _openEvents?.Invoke();
    }
}
