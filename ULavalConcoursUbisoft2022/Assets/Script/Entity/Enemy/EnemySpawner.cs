using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] private GameObject enemy;
    [SerializeField] private int enemyCount;
    [SerializeField] private float spawnZoneRadius;

    private float xPosition;
    private float zPosition;


    public void SpawnEnemies() {
        int spawnedEnemies = 0;
        while (spawnedEnemies < enemyCount) {
            Vector2 position = Random.insideUnitCircle * spawnZoneRadius;

            xPosition = position.x + transform.position.x;
            zPosition = position.y + transform.position.z;

            Instantiate(enemy, new Vector3(xPosition , transform.position.y, zPosition), Quaternion.identity);
            spawnedEnemies++;
        }
    }

}
