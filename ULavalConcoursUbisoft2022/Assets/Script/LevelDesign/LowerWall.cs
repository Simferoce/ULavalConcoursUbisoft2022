using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowerWall : MonoBehaviour
{
    [SerializeField] private Mesh _small = null;
    [SerializeField] private Mesh _large = null;

    private Mesh _originalMesh = null;
    private MeshFilter _meshFilter = null;
    private void Awake()
    {
        _meshFilter = GetComponentInChildren<MeshFilter>();
        UpdateWall();
    }

    public void UpdateWall()
    {
        Camera camera = Camera.main;
        if(camera != null)
        {
            float value = Vector3.Dot(Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized, Vector3.ProjectOnPlane(camera.transform.forward, Vector3.up).normalized);
            if (value > 0)
            {
                _meshFilter.sharedMesh = _small;
            }
            else
            {
                _meshFilter.sharedMesh = _large;
            }
        }
    }
}
