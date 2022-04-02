using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExecuteNamedAction : MonoBehaviour
{
    [Serializable]
    public class Action
    {
        public UnityEvent actions = null;
    }

    [SerializeField] private List<string> _names = new List<string>();
    [SerializeField] private List<Action> _actions = new List<Action>();

    public void ExecuteAction(string action)
    {
        _actions[_names.IndexOf(action)]?.actions?.Invoke();
    }
}
