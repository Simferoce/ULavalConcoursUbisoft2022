using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Awakening : State
{
    [Header("Parameters")]
    [SerializeField] private BoxCollider _spawnRegion = null;
    [SerializeField] private Vector2 _subRegionSize = Vector2.zero;
    [SerializeField] private float _personalSpace = 0.0f;
    [SerializeField] private GameObject _minionPrefab = null;
    [SerializeField] private float _numberOfSpawns = 0;
    [SerializeField] private float _maxEnergy = 0.0f;
    [SerializeField] private float _energyPerSecondPerAlive = 0.0f;
    [SerializeField] private Vector2 _delaySpawn = Vector2.zero;
    [SerializeField] private float _maxTimeAwakening = 0.0f;
    [SerializeField] private bool _skipPhase = false;

    [Header("Reference")]
    [SerializeField] private Entity _entity = null;
    [SerializeField] private bool _randomizePosition = false;

    [Header("State")]
    [SerializeField] private State _transcendentState = null;

    private float _energy = 0.0f;
    private int _numberSpawned = 0;
    private float _enteredTime = 0.0f;
    private Player _player = null;

    public float Energy { get => _energy; set => _energy = value; }

    private List<Health> _minionsStatus = new List<Health>();

    protected override void Init()
    {
        _player = GameObject.FindObjectOfType<Player>();
    }

    protected override void OnEnter()
    {
        _entity.Health.Invicible = true;

        _numberSpawned = 0;

        if (_skipPhase)
        {
            _energy = _maxEnergy;
            return;
        }

        _enteredTime = Time.time;
        StartCoroutine(SpawnMinions());
    }

    private IEnumerator SpawnMinions()
    {
        bool[] canSpawnRegions = new bool[(int)_subRegionSize.x * (int)_subRegionSize.y];
        Vector2 regionSize = new Vector2(_spawnRegion.bounds.size.x / _subRegionSize.y, _spawnRegion.bounds.size.z / _subRegionSize.x);
        Vector3 regionCorner = Vector3.ProjectOnPlane(_spawnRegion.bounds.max, Vector3.up);

        Vector3 playerPositionProjected = Vector3.ProjectOnPlane(_player.transform.position, Vector3.up);
        Vector3 entityPositionProjected = Vector3.ProjectOnPlane(_entity.transform.position, Vector3.up);

        //Lazy Algo
        for (int i = 0; i < canSpawnRegions.Length; ++i)
        {
            Vector3 projectedRegionCenter = new Vector3(regionCorner.x - regionSize.x * (int)(i / _subRegionSize.x) - regionSize.x / 2, 0, regionCorner.z - regionSize.y * (i % _subRegionSize.x) - regionSize.y / 2);
            canSpawnRegions[i] = Vector3.Distance(playerPositionProjected, projectedRegionCenter) > _personalSpace
                && Vector3.Distance(entityPositionProjected, projectedRegionCenter) > _personalSpace;
        }

        if (canSpawnRegions.Count(x => x) < _numberOfSpawns)
        {
            Debug.LogError("Cannot spawn all the number of entities");
        }

        List<int> canSpawnIndex = new List<int>();
        for (int i = 0; i < canSpawnRegions.Length; ++i)
        {
            if (canSpawnRegions[i])
            {
                canSpawnIndex.Add(i);
            }
        }

        int originalSize = canSpawnIndex.Count;
        for (int i = 0; i < Mathf.Min(_numberOfSpawns, originalSize); ++i)
        {
            int regionToSpawn = Random.Range(0, canSpawnIndex.Count - 1);
            Vector2 randomPosition = _randomizePosition ? Random.insideUnitCircle : new Vector2(0, 0);
            Vector3 spawnPosition = new Vector3(randomPosition.x * regionSize.x / 2, 0, randomPosition.y * regionSize.y / 2) + new Vector3(regionCorner.x - regionSize.x * (int)(canSpawnIndex[regionToSpawn] / _subRegionSize.x) - regionSize.x / 2,
                0, regionCorner.z - regionSize.y * (canSpawnIndex[regionToSpawn] % _subRegionSize.x) - regionSize.y / 2);
            GameObject minion = Instantiate(_minionPrefab, spawnPosition, Quaternion.identity);

            Health health = minion.GetComponentInChildren<Health>();
            health.OnDeath.AddListener(MinionOnDeath);
            canSpawnIndex.RemoveAt(regionToSpawn);

            _minionsStatus.Add(health);
            _numberSpawned++;
            yield return new WaitForSeconds(Random.Range(_delaySpawn.x, _delaySpawn.y));
        }
    }

    private void MinionOnDeath(Health health)
    {
        _minionsStatus.Remove(health);
        health.OnDeath.RemoveListener(MinionOnDeath);
    }

    protected override void OnExit()
    {
        _entity.Health.Invicible = false;
    }

    protected override void OnUpdate()
    {
        _energy += _minionsStatus.Count * _energyPerSecondPerAlive * Time.deltaTime;

        if(_energy >= _maxEnergy || (_minionsStatus.Count == 0 && _numberSpawned == _numberOfSpawns) || Time.time - _enteredTime > _maxTimeAwakening)
        {
            foreach(Health health in _minionsStatus.ToList())
            {
                health.Kill();
            }
            _minionsStatus.Clear();

            ChangeState(_transcendentState);
        }
    }
}
