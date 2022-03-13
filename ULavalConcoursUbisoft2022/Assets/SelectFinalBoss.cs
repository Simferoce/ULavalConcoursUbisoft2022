using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SelectFinalBoss : MonoBehaviour
{
    [System.Serializable]
    public class SelectBoss
    {
        [SerializeField] public List<WingRoot.Wing> WingTypes = new List<WingRoot.Wing>();
        [SerializeField] public GameObject SelectedBoss = null;
    }

    [SerializeField] private List<SelectBoss> _selectBoss = new List<SelectBoss>();
    [SerializeField] private GameObject _default = null;

    private GameObject _boss = null;
    private WingRoot[] _wings = null;

    private void Start()
    {
        _wings = GameObject.FindObjectsOfType<WingRoot>();
        GetComponent<Trigger>().actions.AddListener(SpawnBoss);
        _boss = _selectBoss.FirstOrDefault(x => x.WingTypes.All(y => _wings.Any(z => z.WingType == y)))?.SelectedBoss;
        if(_boss == null)
        {
            _boss = _default;
        }
    }

    private void SpawnBoss()
    {
        _boss.SetActive(true);
    }
}
