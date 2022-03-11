using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartAnim : State
{
    [SerializeField] private State _nextState = null;
    [SerializeField] private float _duration = 0.0f;
    [SerializeField] private Entity _entity = null;

    public UnityEvent OnEnterEvent;
    private CameraControl _cameraControl = null;
    private Player _player = null;

    protected override void Init()
    {
        _cameraControl = GameObject.FindObjectOfType<CameraControl>();
        _player = GameObject.FindObjectOfType<Player>();
    }

    protected override void OnEnter()
    {
        OnEnterEvent?.Invoke();
        StartCoroutine(ChangeStateAfterAMoment());
    }

    protected override void OnExit()
    {
        
    }

    protected override void OnUpdate()
    {
        
    }

    IEnumerator ChangeStateAfterAMoment()
    {
        _player.LockControl();
        _cameraControl.Focus(_entity.transform);
        yield return new WaitForSeconds(_duration);
        _cameraControl.ResetToOrigin();
        ChangeState(_nextState);
        _player.UnlockControl();
    }
}
