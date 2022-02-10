using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnTrigger : MonoBehaviour
{

    [SerializeField] private EnemySpawner spawner;
    [SerializeField] private bool hasBeenTriggered = false;
    
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.GetComponent<Player>() != null && !hasBeenTriggered) {
            spawner.SpawnEnemies();
            hasBeenTriggered = true;
        }
    }
}
