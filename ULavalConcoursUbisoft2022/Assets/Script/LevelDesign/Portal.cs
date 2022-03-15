using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private WingRoot.Wing _toWing = WingRoot.Wing.Safe;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            FindObjectsOfType<WingRoot>().FirstOrDefault(x => x.WingType == _toWing)?.GetComponentInChildren<PlayerSpawnPoint>()?.Teleport();
        }
    }
}
