using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DepressingIdle : State
{
    [Header("Parameters")]
    [SerializeField] private Transform _middleRoom = null;
    [SerializeField] private float _minDistance = 0.0f;
    [SerializeField] private int _numberOfTeleportBeforeChangeState = 0;
    [SerializeField] private float _damageBeforeTeleporting = 1.0f;

    [Header("Reference")]
    [SerializeField] private Entity _entity = null;

    [Header("State")]
    [SerializeField] private State _nextState = null;

    public UnityEvent OnTeleport;

    private float _damageReceiveSinceLastTeleport = 0.0f;
    private int _numberOfTeleport = 0;

    protected override void Init()
    {
        _entity.Health.OnDamage += Health_OnDamage;
    }

    private void Health_OnDamage(Health obj, float damage)
    {
        _damageReceiveSinceLastTeleport += damage;
    }

    protected override void OnEnter()
    {
        _numberOfTeleport = 0;
        _damageReceiveSinceLastTeleport = 0.0f;
    }

    protected override void OnExit()
    {
        
    }

    protected override void OnUpdate()
    {
        if(_damageReceiveSinceLastTeleport >= _damageBeforeTeleporting)
        {
            if(_numberOfTeleport >= _numberOfTeleportBeforeChangeState - 1)
            {
                ChangeState(_nextState);
            }
            else
            {
                _damageReceiveSinceLastTeleport = 0;
                OnTeleport?.Invoke();
                _entity.Teleport(FindTeleportPosition(Random.insideUnitCircle.normalized, _entity.transform.position - _middleRoom.position, _minDistance) + _middleRoom.position);
                _numberOfTeleport++;
            }
        }
    }

    //Find a position at X distance of a line with a random slope passing through the center of the room. (Always teleport X distance and stay in the middle of the room)
    private Vector3 FindTeleportPosition(Vector2 direction, Vector3 currentPosition, float distanceTeleport)
    {
        float x = currentPosition.x;
        float r = direction.y / direction.x;
        float y = currentPosition.z;
        float d = distanceTeleport;
        float d2 = d * d;
        float r2 = r * r;
        float x2 = x * x;
        float y2 = y * y;
        float b = 0;

        float newX = (x + y * r + Mathf.Sqrt(d2 * r2 - x2 * r2 + 2 * x * y * r + d2 - y2)) / (1 + r2);
        float newY = r * newX + b;

        float newX2 = (x + y * r - Mathf.Sqrt(d2 * r2 - x2 * r2 + 2 * x * y * r + d2 - y2)) / (1 + r2);
        float newY2 = r * newX2 + b;

        Vector3 pos1 = new Vector3(newX, currentPosition.y, newY);
        Vector3 pos2 = new Vector3(newX2, currentPosition.y, newY2);
        Vector3 pos = Vector3.Distance(Vector3.zero, Vector3.ProjectOnPlane(pos1, Vector3.up)) 
            > Vector3.Distance(Vector3.zero, Vector3.ProjectOnPlane(pos2, Vector3.up)) 
            ? pos2 : pos1;

        //Debug.Log($"Current: {currentPosition} Pos: {pos} Dir: {direction}, Dist {Vector3.Distance(currentPosition, pos)} Distance from center: {Vector3.Distance(Vector3.ProjectOnPlane(_middleRoom.position, Vector3.up), Vector3.ProjectOnPlane(_entity.transform.position, Vector3.up))}");

        return pos;
    }
}
