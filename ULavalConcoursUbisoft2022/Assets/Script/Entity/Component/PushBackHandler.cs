using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBackHandler : MonoBehaviour
{
    [SerializeField] private float _frictionPercentagePerSecond = 0.0f;
    [SerializeField] private Entity _entity = null;
    [SerializeField] private List<ForceZone> _forceZones = new List<ForceZone>();

    [System.Serializable]
    private class ForceZone
    {
        public Vector3 Origin;
        public float Radius;
        public float Force;
        public float Effective = 1;
        public Vector3 GetForce(Vector3 position)
        {
            Vector3 positionOnPlaneOrigin = Vector3.ProjectOnPlane(Origin, Vector3.up);
            Vector3 projectionOnPlane = Vector3.ProjectOnPlane(position, Vector3.up);
            float distance = Vector3.Distance(positionOnPlaneOrigin, projectionOnPlane);

            return (projectionOnPlane - positionOnPlaneOrigin).normalized * (1 - (distance / Radius)) * Force * Effective;
        }
    }

    public void AddForceZone(Vector3 origin, float force, float radius)
    {
        _forceZones.Add(new ForceZone(){Force = force, Origin = origin, Radius = radius });
    }

    private void Update()
    {
        foreach (ForceZone _zone in _forceZones)
        {
            _entity.Move(_zone.GetForce(_entity.transform.position) * Time.deltaTime);
            _zone.Effective *= 1 - _frictionPercentagePerSecond * Time.deltaTime;
        }

        _forceZones.RemoveAll(x => x.Effective < 0.1f);
    }
}
