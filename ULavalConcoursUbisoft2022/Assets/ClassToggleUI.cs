using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClassToggleUI : MonoBehaviour
{
    [SerializeField] private ClassData _class = null;
    [SerializeField] private TextMeshProUGUI _className = null;
    [SerializeField] private TextMeshProUGUI _classDescription = null;
    [SerializeField] private TextMeshProUGUI _statistic = null;
    [SerializeField] private Image _icon = null;

    public ClassData Class { get => _class; set => _class = value; }

    private void Awake()
    {
        _className.text = _class.Name;
        _classDescription.text = _class.Description;
        _statistic.text = _class.StatisticDetail;
        _icon.sprite = _class.Icon;
    }
}
