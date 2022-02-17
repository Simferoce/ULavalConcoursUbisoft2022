using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Indicator : MonoBehaviour
{
    [SerializeField] protected DecalProjector _projector = null;

    protected float _timeBeforeImpact = 0.0f;
    protected bool _initialized = false;
    protected float _startTime = 0.0f;
    protected Transform _followDirection = null;

    public virtual void Init(float timeBeforeImpact, Vector2 size, Transform followDirection = null)
    {
        _timeBeforeImpact = timeBeforeImpact;
        _startTime = Time.time;
        _initialized = true;
        _projector.transform.localScale = Vector3.zero;
        _followDirection = followDirection;
        _projector.size = new Vector3(size.x, size.y, 20);
    }
}
