using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    private enum StateEnum
    {
        Wander = 0,
        Agressive = 1,
        Flee = 2,
    }

    private abstract class State
    {
        protected MonoBehaviour _context = null;

        public virtual void Init(MonoBehaviour context)
        {
            _context = context;
        }
        public abstract void Enter();
        public abstract int Update();
        public abstract void Exit();
    }

    [Serializable]
    private class StateMachine
    {
        private State[] states;
        [SerializeField] private int currentStateIndex = -1;

        public void Init(State[] states, int startIndex, MonoBehaviour context)
        {
            this.states = states;
            foreach(State state in states)
            {
                state.Init(context);
            }

            currentStateIndex = startIndex;
            states[currentStateIndex].Enter();
        }

        public void Update()
        {
            int nextIndex = states[currentStateIndex].Update();
            if (nextIndex != -1)
            {
                states[currentStateIndex].Exit();
                currentStateIndex = nextIndex;
                states[currentStateIndex].Enter();
            }
        }
    }

    [Serializable]
    private class Flee : State
    {
        [SerializeField] private float _distanceStopFleeing = 0.0f;
        [SerializeField] private NavMeshAgent _navMeshAgent = null;
        [SerializeField] private float _speed = 0.0f;

        private Player _player = null;
        private bool _movingBackward = false;

        public override void Enter()
        {
            _navMeshAgent.speed = _speed;
        }

        public override void Exit()
        {
            
        }

        public override void Init(MonoBehaviour context)
        {
            base.Init(context);
            _player = FindObjectOfType<Player>();
        }

        public override int Update()
        {
            Vector3 playerPositionOnPlane = Vector3.ProjectOnPlane(_player.transform.position, Vector3.up);
            Vector3 aiPositionOnPlane = Vector3.ProjectOnPlane(_context.transform.position, Vector3.up);

            
            if(Vector3.Distance(playerPositionOnPlane, aiPositionOnPlane) > _distanceStopFleeing)
            {
                return (int)StateEnum.Wander;
            }

            _navMeshAgent.SetDestination(FindBackwardPosition(_context.transform.position, (aiPositionOnPlane - playerPositionOnPlane).normalized));
            
            return -1;
        }

        private Vector3 FindBackwardPosition(Vector3 origin, Vector3 direction)
        {
            Vector3 position = origin;

            RaycastHit hit;
            if (Physics.Raycast(origin, direction, out hit, 3))
            {
                position = hit.point;
            }
            else
            {
                position = origin + direction;
            }
            return position;
        }
    }

    [Serializable]
    private class Wander : State
    {
        [SerializeField] private float _distanceDetectPlayer = 0.0f;
        [SerializeField] private float _MaxRangeWander = 0.0f;
        [SerializeField] private NavMeshAgent _navMeshAgent = null;
        [SerializeField] private Vector2 delayWander = Vector2.zero;
        [SerializeField] private float _speed = 0.0f;

        private Player _player = null;
        private float _lastWander = 0.0f;
        private float _currentOffSetWander = 0.0f;

        private Vector3 _origin = Vector3.zero;
        public override void Enter()
        {
            _currentOffSetWander = UnityEngine.Random.Range(delayWander.x, delayWander.y);
            _navMeshAgent.speed = _speed;
        }

        public override void Exit()
        {

        }

        public override void Init(MonoBehaviour context)
        {
            base.Init(context);
            _player = FindObjectOfType<Player>();
            _origin = _context.transform.position;
        }

        public override int Update()
        {
            Vector3 playerPositionOnPlane = Vector3.ProjectOnPlane(_player.transform.position, Vector3.up);
            Vector3 aiPositionOnPlane = Vector3.ProjectOnPlane(_context.transform.position, Vector3.up);

            if (Vector3.Distance(playerPositionOnPlane, aiPositionOnPlane) < _distanceDetectPlayer)
            {
                return (int)StateEnum.Agressive;
            }

            if(Time.time - _currentOffSetWander > _lastWander)
            {
                Vector3 destination = FindWanderPosition();
                _lastWander = Time.time;
                _currentOffSetWander = UnityEngine.Random.Range(delayWander.x, delayWander.y);

                RaycastHit hit;
                if (Physics.Raycast(_context.transform.position, (destination - _context.transform.position).normalized, out hit, (destination - _context.transform.position).magnitude))
                {
                    destination = hit.point;
                }

                _navMeshAgent.SetDestination(destination);
            }

            return -1;
        }

        private Vector3 FindWanderPosition()
        {
            Vector2 randomInCircle = UnityEngine.Random.insideUnitCircle * _MaxRangeWander;
            return _origin + new Vector3(randomInCircle.x, 0, randomInCircle.y);
        }
    }

    [Serializable]
    private class Agressive : State
    {
        [SerializeField] private NavMeshAgent _navMeshAgent = null;
        [SerializeField] private float _speed = 0.0f;
        [SerializeField] private float _startFleeingRange = 0.0f;
        [SerializeField] private float _stopChasingPlayerRange = 0.0f;
        [SerializeField] private Entity _entity = null;
        [SerializeField] private float _distanceKeptBetweenItselfAndPlayer = 0.0f;

        private Player _player = null;

        public override void Enter()
        {
            _navMeshAgent.speed = _speed;
        }

        public override void Exit()
        {
            
        }

        public override void Init(MonoBehaviour context)
        {
            base.Init(context);
            _player = FindObjectOfType<Player>();
        }

        public override int Update()
        {
            Vector3 playerPositionOnPlane = Vector3.ProjectOnPlane(_player.transform.position, Vector3.up);
            Vector3 aiPositionOnPlane = Vector3.ProjectOnPlane(_context.transform.position, Vector3.up);

            Vector3 destination = _context.transform.position;

            RaycastHit hit;
            bool seePlayer = !Physics.SphereCast(_context.transform.position, 0.1f, playerPositionOnPlane - aiPositionOnPlane, out hit, (playerPositionOnPlane - aiPositionOnPlane).magnitude, LayerMask.GetMask("Wall"));

            if(seePlayer)
            {
                destination = (aiPositionOnPlane - playerPositionOnPlane).normalized * _distanceKeptBetweenItselfAndPlayer + playerPositionOnPlane + new Vector3(0, _context.transform.position.y, 0);
            }
            else
            {
                destination = _player.transform.position;
            }

            _navMeshAgent.SetDestination(destination);

            LookTowardsPlayer(_player.transform.position, _context.transform);
            if (seePlayer && _entity.CanAttack())
            {
                _entity.Attack();
            }

            if (seePlayer && Vector3.Distance(playerPositionOnPlane, aiPositionOnPlane) < _startFleeingRange)
            {
                return (int)StateEnum.Flee;
            }

            if (Vector3.Distance(playerPositionOnPlane, aiPositionOnPlane) > _stopChasingPlayerRange)
            {
                return (int)StateEnum.Wander;
            }



            return -1;
        }

        private void LookTowardsPlayer(Vector3 target, Transform transform)
        {
            transform.LookAt(new Vector3(target.x, transform.position.y, target.z));
        }
    }

    [SerializeField] private Wander _wanderState = null;
    [SerializeField] private Flee _fleeState = null;
    [SerializeField] private Agressive _agressive = null;

    [SerializeField] private StateMachine _stateMachine = new StateMachine();

    private void Awake()
    {
        _stateMachine.Init(new State[] { _wanderState, _agressive, _fleeState }, 0, this);
    }

    private void Update()
    {
        _stateMachine.Update();
    }

   
}
