using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // Certifique-se de que esta linha está presente

public class WaveManager : MonoBehaviour
{
    public Level[] levels;
    public int gameLevelScene = 1; // Assumindo que esta é a primeira cena de nível
    private int currentLevel = 0;
    public EnemySpawner spawner;
    private bool levelInProgress = false;
    public LevelManager main;

    private void Update()
    {
        if (!levelInProgress && currentLevel < levels.Length)
        {
            StartCoroutine(StartNextLevel(levels[currentLevel]));
        }

        if (currentLevel >= levels.Length)
        {
            Debug.Log("Todos os níveis foram completados, precisa trocar de cena ou reiniciar o jogo!");
            NextSceneOrRestart(); // Chama o método para decidir o próximo passo
        }
    }

    IEnumerator StartNextLevel(Level level)
    {
        levelInProgress = true;
        
        // Implemente suas configurações específicas de cada nível aqui
        ConfigureLevelSettings(gameLevelScene, currentLevel, level);

        for (int i = 0; i < level.numberOfWaves; i++)
        {
            spawner.enemyPrefab = level.enemyPrefab;                                    
            yield return StartCoroutine(spawner.SpawnEnemies(level.spawnInterval, level.enemiesPerWave)); 
            yield return new WaitForSeconds(level.timeBetweenWaves);
        }
        currentLevel++;
        levelInProgress = false;
    }

    private void ConfigureLevelSettings(int gameLevel, int levelIndex, Level level)
    {
        // Configurações do nível, simplificado para melhor legibilidade
        if (gameLevel == 1 && levelIndex == 0){
            level.enemiesPerWave = 5;
            level.numberOfWaves = 2;
            level.timeBetweenWaves = 10;
            level.spawnInterval = 5;
        } else if (gameLevel == 1 && levelIndex == 1){
            level.enemiesPerWave = 10;
            level.numberOfWaves = 2;
            level.timeBetweenWaves = 10;
            level.spawnInterval = 2;
        } else if (gameLevel == 1 && levelIndex == 2){
            level.enemiesPerWave = 20;
            level.numberOfWaves = 2;
            level.timeBetweenWaves = 10;
            level.spawnInterval = 1;
        } else if (gameLevel == 1 && levelIndex == 3){
            level.enemiesPerWave = 25;
            level.numberOfWaves = 3;
            level.timeBetweenWaves = 5;
            level.spawnInterval = .5f;
        } else if (gameLevel == 2 && levelIndex == 0){
            level.enemiesPerWave = 5;
            level.numberOfWaves = 2;
            level.timeBetweenWaves = 10;
            level.spawnInterval = 5;
        } else if (gameLevel == 2 && levelIndex == 1){
            level.enemiesPerWave = 10;
            level.numberOfWaves = 2;
            level.timeBetweenWaves = 10;
            level.spawnInterval = 2;
        } else if (gameLevel == 2 && levelIndex == 2){
            level.enemiesPerWave = 20;
            level.numberOfWaves = 2;
            level.timeBetweenWaves = 10;
            level.spawnInterval = 1;
        } else if (gameLevel == 1 && levelIndex == 3){
            level.enemiesPerWave = 30;
            level.numberOfWaves = 5;
            level.timeBetweenWaves = 5;
            level.spawnInterval = .2f;
        }
    }

    private void NextSceneOrRestart()
    {
        int totalScenes = SceneManager.sceneCountInBuildSettings; // Total de cenas disponíveis
        if (SceneManager.GetActiveScene().buildIndex + 1 < totalScenes)
        {
            // Ainda há cenas para jogar, vai para a próxima cena
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            // Não há mais cenas, volta para o menu
            SceneManager.LoadScene("Menu"); // Certifique-se de que "Menu" é o nome correto
        }
    }
}
