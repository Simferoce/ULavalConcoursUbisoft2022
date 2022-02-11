using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] private GameObject[] enemies;
    [SerializeField] private int minEnemyCount;
    [SerializeField] private int maxEnemyCount;
    [SerializeField] private float spawnZoneRadius;

    private float xPosition;
    private float zPosition;


    public void SpawnEnemies() {
        int spawnedEnemies = 0;
        int enemyCount = Random.Range(minEnemyCount, maxEnemyCount);
        GameObject selectedEnemy = enemies[Random.Range(0, enemies.Length)];
        while (spawnedEnemies < enemyCount) {
            Vector2 position = Random.insideUnitCircle * spawnZoneRadius;

            xPosition = position.x + transform.position.x;
            zPosition = position.y + transform.position.z;

            Instantiate(selectedEnemy, new Vector3(xPosition , transform.position.y, zPosition), Quaternion.identity);
            spawnedEnemies++;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.1f);
        Gizmos.DrawSphere(transform.position, spawnZoneRadius);
    }

}
