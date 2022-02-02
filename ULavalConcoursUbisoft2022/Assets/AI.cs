using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    [SerializeField] private float _distanceStartChasing = 0.0f;
    [SerializeField] private float _distanceStopChasing = 0.0f;
    [SerializeField] private NavMeshAgent _navMeshAgent = null;

    private bool _chasing = false;

    private Player _player = null;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        Application.targetFrameRate = 60;
    }

    private void Update()
    {
        Vector2 playerPosition2D = new Vector2(_player.transform.position.x, _player.transform.position.z);
        Vector2 aiPosition2D = new Vector2(this.transform.position.x, this.transform.position.z);
        if (_chasing && Vector2.Distance(playerPosition2D, aiPosition2D) > _distanceStopChasing)
        {
            _chasing = false;
        }
        else if(!_chasing && Vector2.Distance(playerPosition2D, aiPosition2D) < _distanceStartChasing)
        {
            _chasing = true;
        }

        if (_chasing)
        {
            _navMeshAgent.SetDestination(_player.transform.position);
        }
    }

    private void FixedUpdate()
    {
        
    }
}
