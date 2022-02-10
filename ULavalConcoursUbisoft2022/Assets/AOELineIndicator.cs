using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOELineIndicator : Indicator
{
    private Vector3 _size = Vector3.zero;

    private float _groundHeight = 0.0f;
    public override void Init(float timeBeforeImpact, Vector3 size, Transform followDirection = null)
    {
        base.Init(timeBeforeImpact, size, followDirection);

        _size = size;
        _projector.size = new Vector3(2, 0, 10);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, _size.x, LayerMask.GetMask("Ground")))
        {
            _groundHeight = hit.point.y;
        }
    }

    private void Update()
    {
        if (_initialized)
        {
            transform.parent.forward = _followDirection.forward;

            float fullSize = _size.x;

            Vector3 positionAtGroundLevel = new Vector3(transform.position.x, _groundHeight + 0.1f, transform.position.z);
            Debug.DrawLine(positionAtGroundLevel, positionAtGroundLevel + transform.parent.forward * _size.x, Color.blue, 1);
            RaycastHit hit;
            if (Physics.Raycast(positionAtGroundLevel, transform.parent.forward, out hit, _size.x, LayerMask.GetMask("Wall")))
            {
                fullSize = Vector3.Distance(Vector3.ProjectOnPlane(transform.position, Vector3.up), Vector3.ProjectOnPlane(hit.point, Vector3.up));
            }


            float _percentageTimeBeforeImpact = (Time.time - _startTime) / _timeBeforeImpact;
            _projector.pivot = new Vector3(0, fullSize / 2, 0);
            _projector.size = new Vector3(2, fullSize, 10);

            
            if (_percentageTimeBeforeImpact > 1)
            {
                Destroy(this.transform.parent.gameObject);
            }
            else
            {
                _projector.transform.localScale = new Vector3(1, _percentageTimeBeforeImpact, 1);
            }
        }
    }
}
