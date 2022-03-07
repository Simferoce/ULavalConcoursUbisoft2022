using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private Transform _cameraTarget = null;
    [SerializeField] private Transform _player = null;
    [SerializeField] private CinemachineVirtualCamera _virtualCamera = null;
    [SerializeField] private CinemachineVirtualCamera _virtualUICamera = null;
    [SerializeField] private Transform _aim = null;
    [SerializeField] private Camera _mainCamera = null;
    [Tooltip("In screen space coordinate (0...1) (0...1)")]
    [SerializeField] private float _borderRadius = 0.0f;

    private Plane _plane = new Plane(Vector3.up, -1);
    private bool _focus = false;

    public void Focus(Transform transform)
    {
        _virtualCamera.Follow = transform;
        _virtualCamera.LookAt = transform;

        _virtualUICamera.Follow = transform;
        _virtualUICamera.LookAt = transform;

        _focus = true;
    }

    public void ResetToOrigin()
    {
        _virtualCamera.Follow = _cameraTarget;
        _virtualCamera.LookAt = _cameraTarget;

        _virtualUICamera.Follow = _cameraTarget;
        _virtualUICamera.LookAt = _cameraTarget;

        _focus = false;
    }

    private void Update()
    {
        if(!_focus)
        {
            Vector3 viewportPosition = _mainCamera.ScreenToViewportPoint(Input.mousePosition);
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            float enter = 0.0f;
            if (_plane.Raycast(ray, out enter))
            {
                Vector3 hitPoint = ray.GetPoint(enter);

                _aim.position = hitPoint;
            }

            if (Vector2.Distance(new Vector2(viewportPosition.x, viewportPosition.y), new Vector2(0.5f, 0.5f)) > _borderRadius)
            {
                _virtualCamera.Follow = _cameraTarget;
                _virtualCamera.LookAt = _cameraTarget;

                _virtualUICamera.Follow = _cameraTarget;
                _virtualUICamera.LookAt = _cameraTarget;
            }
            else
            {
                _virtualCamera.Follow = _player;
                _virtualCamera.LookAt = _player;

                _virtualUICamera.Follow = _player;
                _virtualUICamera.LookAt = _player;
            }
        }
    }

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(_aim.position, 0.3f);
    }
}
