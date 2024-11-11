using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory
{
    private GameObject enemyPrefab;

    public EnemyFactory(GameObject prefab)
    {
        enemyPrefab = prefab;
    }

    public GameObject CreateEnemy(Vector3 position, Quaternion rotation, Transform destination)
    {
        GameObject enemy = GameObject.Instantiate(enemyPrefab, position, rotation);

        // Configura el movimiento y destino del enemigo
        EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();
        if (enemyMovement != null)
        {
            enemyMovement.SetDestination(destination);
        }

        return enemy;
    }
}
