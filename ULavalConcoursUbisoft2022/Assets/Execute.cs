using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Execute : MonoBehaviour
{
    [Serializable]
    public class Action
    {
        public UnityEvent actions = null;
    }

    [SerializeField] private List<Action> _actions = new List<Action>();

    public void ExecuteAction(int index)
    {
        _actions[index].actions?.Invoke();
    }
}
