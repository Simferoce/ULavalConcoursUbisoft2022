using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class AOECircleIndicator : Indicator
{
    public override void Init(float timeBeforeImpact, Vector2 size, Transform followDirection = null)
    {
        base.Init(timeBeforeImpact, size, followDirection);
    }

    private void Update()
    {
        if (_initialized)
        {
            float _percentageTimeBeforeImpact = (Time.time - _startTime) / _timeBeforeImpact;
            if (_percentageTimeBeforeImpact > 1)
            {
                Destroy(_source);
            }
            else
            {
                _projector.transform.localScale = Vector3.one * _percentageTimeBeforeImpact;
            }
        }
    }
}
