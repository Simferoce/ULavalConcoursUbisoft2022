using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                _entity.Teleport(FindTeleportPosition(new Vector2(_middleRoom.position.x, _middleRoom.position.z), Random.insideUnitCircle.normalized, _entity.transform.position, _minDistance));
                _numberOfTeleport++;
            }
        }
    }

    //Find a position at X distance of a line with a random slope passing through the center of the room. (Always teleport X distance and stay in the middle of the room)
    private Vector3 FindTeleportPosition(Vector3 roomCenter, Vector2 direction, Vector3 currentPosition, float distanceTeleport)
    {
        float x = currentPosition.x;
        float r = direction.y / direction.x;
        float y = currentPosition.z;
        float d = distanceTeleport;
        float d2 = d * d;
        float r2 = r * r;
        float x2 = x * x;
        float y2 = y * y;
        float b = _middleRoom.position.x - r * _middleRoom.position.z;
        float b2 = b * b;

        float newX = (x - b * r + y * r + Mathf.Sqrt(d2 * r2 - x2 * r2 + 2 * x * y * r - 2 * b * x * r + d2 + 2 * b * y - b2 - y2)) / (1 + r2);
        float newY = r * newX + b;

        float newX2 = (x - b * r + y * r - Mathf.Sqrt(d2 * r2 - x2 * r2 + 2 * x * y * r - 2 * b * x * r + d2 + 2 * b * y - b2 - y2)) / (1 + r2);
        float newY2 = r * newX2 + b;

        Vector3 pos1 = new Vector3(newX, currentPosition.y, newY);
        Vector3 pos2 = new Vector3(newX2, currentPosition.y, newY2);
        Vector3 pos = Vector3.Distance(Vector3.ProjectOnPlane(_middleRoom.position, Vector3.up), Vector3.ProjectOnPlane(pos1, Vector3.up)) 
            > Vector3.Distance(Vector3.ProjectOnPlane(_middleRoom.position, Vector3.up), Vector3.ProjectOnPlane(pos2, Vector3.up)) 
            ? pos2 : pos1;

        //Debug.Log($"Current: {currentPosition} Pos: {pos} Dir: {direction}, Dist {Vector3.Distance(currentPosition, pos)} Distance from center: {Vector3.Distance(Vector3.ProjectOnPlane(_middleRoom.position, Vector3.up), Vector3.ProjectOnPlane(_entity.transform.position, Vector3.up))}");

        return pos;
    }
}
