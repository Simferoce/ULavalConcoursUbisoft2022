using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOELineIndicator : Indicator
{
    private Vector3 _size = Vector3.zero;

    [SerializeField] private bool _colliderWithWall = true;

    private float _groundHeight = 0.0f;
    public override void Init(float timeBeforeImpact, Vector2 size, Transform followDirection = null)
    {
        base.Init(timeBeforeImpact, size, followDirection);

        _size = new Vector3(size.x, size.y, 20);
        _projector.size = new Vector3(_size.x, 0, _size.z);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 10, LayerMask.GetMask("Ground")))
        {
            _groundHeight = hit.point.y;
        }
    }

    private void Update()
    {
        if (_initialized)
        {
            if(_followDirection != null)
            {
                _source.transform.forward = _followDirection.forward;
            }

            float fullSize = _size.y;

            Vector3 positionAtGroundLevel = new Vector3(transform.position.x, _groundHeight + 0.1f, transform.position.z);

            if (_colliderWithWall)
            {
                RaycastHit hit;
                if (Physics.Raycast(positionAtGroundLevel, _source.transform.forward, out hit, _size.y, LayerMask.GetMask("Wall")))
                {
                    fullSize = Vector3.Distance(Vector3.ProjectOnPlane(transform.position, Vector3.up), Vector3.ProjectOnPlane(hit.point, Vector3.up));
                }
            }
            
            float _percentageTimeBeforeImpact = (Time.time - _startTime) / _timeBeforeImpact;
            _projector.pivot = new Vector3(0, fullSize / 2, 0);
            _projector.size = new Vector3(_size.x, fullSize, 10);
            _borderProjector.pivot = new Vector3(0, fullSize / 2, 0);
            _borderProjector.size = new Vector3(_size.x, fullSize, 10);

            if (_percentageTimeBeforeImpact > 1)
            {
                Destroy(_source);
            }
            else
            {
                _projector.transform.localScale = new Vector3(1, _percentageTimeBeforeImpact, 1);
            }
        }
    }
}
