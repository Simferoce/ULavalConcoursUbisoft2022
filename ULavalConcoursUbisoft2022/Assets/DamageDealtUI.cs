using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealtUI : MonoBehaviour
{
    [SerializeField] private GameObject _prefab = null;
    [SerializeField] private float _radiusOffSet = 0.0f;

    public void OnDamageDealt(object parameters)
    {
        Health.HealthDamageTakenDTO dto = (Health.HealthDamageTakenDTO) parameters;
        if(dto.SourceTeam == Entity.Team.Friend)
        {
            GameObject number = Instantiate(_prefab, dto.Health.transform.position + Random.insideUnitSphere * _radiusOffSet, Quaternion.identity);
            number.GetComponent<DamageNumber>().SetText((int)dto.Damage);
        }
    }
}
