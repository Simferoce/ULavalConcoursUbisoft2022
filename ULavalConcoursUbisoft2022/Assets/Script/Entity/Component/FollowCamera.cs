using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Vector3 _axis = Vector3.one;

    private Camera _cam = null;

    private void Start()
    {
        _cam = Camera.main;
    }

    private void Update()
    {
        transform.rotation = Quaternion.Euler(Vector3.Scale((Quaternion.Inverse(_cam.transform.rotation)).eulerAngles, _axis));
    }
}
