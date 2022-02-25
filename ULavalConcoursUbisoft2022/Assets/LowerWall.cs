using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowerWall : MonoBehaviour
{
    [SerializeField] private Mesh _smallWall = null;

    public void UpdateWall()
    {
        Camera camera = Camera.main;
        float value = Vector3.Dot(Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized, Vector3.ProjectOnPlane(camera.transform.forward, Vector3.up).normalized);
        if (value > 0)
        {
            GetComponent<MeshFilter>().sharedMesh = _smallWall;
        }
    }
}
