using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private Transform _followOrigin = null;
    [SerializeField] private CinemachineVirtualCamera _virtualCamera = null;

    public void Focus(Transform transform)
    {
        _virtualCamera.Follow = transform;
        _virtualCamera.LookAt = transform;
    }

    public void ResetToOrigin()
    {
        _virtualCamera.Follow = _followOrigin;
        _virtualCamera.LookAt = _followOrigin;
    }
}
