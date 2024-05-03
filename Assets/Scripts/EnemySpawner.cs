using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; 
    public Transform spawnPoint;   
    public float minSpawnInterval = 1f;
    public float maxSpawnInterval = 2f;

    public IEnumerator SpawnEnemies(float spawnInterval, int numberOfEnemies)
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(
                spawnInterval); 
        }
    }
}
