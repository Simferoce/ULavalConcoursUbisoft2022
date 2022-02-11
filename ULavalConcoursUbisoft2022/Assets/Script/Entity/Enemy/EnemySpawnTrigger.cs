using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnTrigger : MonoBehaviour
{

    [SerializeField] private EnemySpawner[] spawners;
    
    private bool hasBeenTriggered = false;
    
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.GetComponent<Player>() != null && !hasBeenTriggered) {
            foreach (EnemySpawner spawner in spawners) {
                spawner.SpawnEnemies();
            }
            hasBeenTriggered = true;
        }
    }
}
