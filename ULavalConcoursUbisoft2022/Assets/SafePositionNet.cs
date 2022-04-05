using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafePositionNet : MonoBehaviour
{
    [SerializeField] private BoxCollider _collider = null;
    public void Update()
    {
        if(!_collider.bounds.Contains(transform.position))
        {
            transform.position = _collider.ClosestPointOnBounds(transform.position);
            Physics.SyncTransforms();
        }
    }
}
