using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SummonSkill : Skill
{
    [Serializable]
    private class SpawnGroup
    {
        public List<Transform> points = new List<Transform>();
    }

    [Header("Parameters")]
    [SerializeField] private int _numberToSummon = 0;
    [SerializeField] private GameObject _summonPrefab = null;

    [Header("Reference")]
    [SerializeField] private Entity _entity = null;
    [SerializeField] private List<SpawnGroup> _spawns = new List<SpawnGroup>();

    public int NumberToSummon { get => _numberToSummon; set => _numberToSummon = value; }

    private List<GameObject> _minions = new List<GameObject>();

    public override bool CanUse()
    {
        return true;
    }

    public override void Use()
    {
        base.Use();
        Vector3 entityPosition = Vector3.ProjectOnPlane(_entity.transform.position, Vector3.up);
        SpawnGroup farthest = _spawns.Aggregate((x, y) => 
            Vector3.Distance(entityPosition, Vector3.ProjectOnPlane(x.points.Aggregate(Vector3.zero, (a, b) => a + b.position) / x.points.Count, Vector3.up)) > 
            Vector3.Distance(entityPosition, Vector3.ProjectOnPlane(y.points.Aggregate(Vector3.zero, (a, b) => a + b.position) / y.points.Count, Vector3.up)) ? x : y);

        List<Transform> points = new List<Transform>(farthest.points);

        for(int i = 0; i < _numberToSummon; ++i)
        {
            int index = UnityEngine.Random.Range(0, points.Count);
            GameObject minion = Instantiate(_summonPrefab, points[index].position, Quaternion.identity);
            _minions.Add(minion);
            points.RemoveAt(index);

            if(points.Count == 0)
            {
                points = new List<Transform>(farthest.points);
            }
        }

        InvokeOnSkillFinish();
    }

    public void OnDestroy()
    {
        foreach (GameObject obj in _minions)
        {
            Destroy(obj);
        }

        _minions.Clear();
    }
}
