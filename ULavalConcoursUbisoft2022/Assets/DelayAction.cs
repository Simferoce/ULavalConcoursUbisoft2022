using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DelayAction : MonoBehaviour
{
    [SerializeField] private UnityEvent _actions = null;
    [SerializeField] private float _timer = 0.0f;

    public void Execute()
    {
        StartCoroutine(Action());
    }

    private IEnumerator Action()
    {
        yield return new WaitForSeconds(_timer);
        _actions?.Invoke();
    }
}
