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
        public bool Active = true;
        public UnityEvent actions = null;
    }

    [SerializeField] private List<string> _names = new List<string>();
    [SerializeField] private List<Action> _actions = new List<Action>();

    public void ExecuteAction(string action)
    {
        Action actionObj = _actions[_names.IndexOf(action)];
        if(actionObj.Active)
        {
            actionObj?.actions?.Invoke();
        }
    }

    public void ActivateAction(string action)
    {
        Action actionObj = _actions[_names.IndexOf(action)];
        actionObj.Active = true;
    }
}
