using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedLife : MonoBehaviour
{
    [Tooltip("-1 => infinity")]
    [SerializeField] private float _timeToLive = 0.0f;

    public float TimeToLive { get => _timeToLive; }

    private void Awake()
    {
        if (_timeToLive != -1)
        {
            Destroy(this.gameObject, _timeToLive);
        }
    }
}
