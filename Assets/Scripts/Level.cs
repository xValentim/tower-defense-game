using UnityEngine;

[System.Serializable]
public class Level
{
    public int numberOfWaves; 
    public int enemiesPerWave;
    public GameObject enemyPrefab; 
    public float spawnInterval; 
    public float timeBetweenWaves; 
}
