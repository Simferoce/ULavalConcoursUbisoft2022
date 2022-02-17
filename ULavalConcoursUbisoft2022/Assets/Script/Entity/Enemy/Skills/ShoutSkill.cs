using System.Collections;
using UnityEngine;

public class ShoutSkill : Skill
{
    [Header("Parameters")]
    [SerializeField] private float _radius = 0.0f;
    [SerializeField] private float _force = 0.0f;
    [SerializeField] private float _cooldown = 0.0f;
    [SerializeField] private float _duration = 0.0f;

    [Header("Reference")]
    [SerializeField] private Entity _entity = null;


    private float _lastTimeUsed = 0.0f;
    private Entity[] _entities = null;

    private void Awake()
    {
        _lastTimeUsed = -_cooldown - _duration;
    }

    public override bool CanUse()
    {
        return Time.time - _lastTimeUsed - _duration > _cooldown;
    }

    private void Update()
    {

    }

    public override void Use()
    {
        _entities = GameObject.FindObjectsOfType<Entity>();

        Vector3 positionOnPlane = Vector3.ProjectOnPlane(_entity.transform.position, Vector3.up);
        foreach (Entity entity in _entities)
        {
            Vector3 projectionOnPlaneEntity = Vector3.ProjectOnPlane(entity.transform.position, Vector3.up);
            float distance = Vector3.Distance(projectionOnPlaneEntity, positionOnPlane);
            if (distance < _radius)
            {
                if (entity.PushBackHandler != null)
                {
                    entity.PushBackHandler.AddForceZone(_entity.transform.position, _force, _radius);
                }
            }
        }

        _lastTimeUsed = Time.time;
        StartCoroutine(WaitDuration());
    }

    private IEnumerator WaitDuration()
    {
        yield return new WaitForSeconds(_duration);
        InvokeOnSkillFinish();
    }
}
