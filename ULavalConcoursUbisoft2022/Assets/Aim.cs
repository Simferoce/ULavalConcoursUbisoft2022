using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    [SerializeField] private Camera _camera = null;
    private Plane _plane = new Plane(Vector3.up, 0);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        float enter = 0.0f;
        if (_plane.Raycast(ray, out enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter);

            this.transform.position = hitPoint;
        }
    }

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 0.3f);
    }
}
