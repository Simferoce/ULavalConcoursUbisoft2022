using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageNumber : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text = null;

    public void SetText(int damage)
    {
        _text.text = "-" + damage.ToString();
    }
}
