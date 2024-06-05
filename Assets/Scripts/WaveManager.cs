using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveManager : MonoBehaviour {
    public Level[] levels;
    public int gameLevelScene = 1;
    private int currentLevel = 0;
    private int currentWave = 0; // Variável para rastrear a wave atual
    public EnemySpawner spawner;
    private bool levelInProgress = false;

    private void Update() {
        if (!levelInProgress && currentLevel < levels.Length) {
            StartCoroutine(StartNextLevel(levels[currentLevel], currentWave));
        }

        if (currentLevel >= levels.Length) {
            Debug.Log("Todos os níveis foram completados, precisa trocar de cena ou reiniciar o jogo!");
            NextSceneOrRestart();
        }
    }

    IEnumerator StartNextLevel(Level level, int startWave) {
        levelInProgress = true;

        ConfigureLevelSettings(gameLevelScene, currentLevel, level);

        for (int i = startWave; i < level.numberOfWaves; i++) {
            currentWave = i; // Atualiza a wave atual
            spawner.enemyPrefab = level.enemyPrefab;
            yield return StartCoroutine(spawner.SpawnEnemies(level.spawnInterval, level.enemiesPerWave));
            yield return new WaitForSeconds(level.timeBetweenWaves);
        }
        currentLevel++;
        currentWave = 0; // Reinicia a wave para o próximo nível
        levelInProgress = false;
    }

    private void ConfigureLevelSettings(int gameLevel, int levelIndex, Level level) {
        // Configurações do nível, simplificado para melhor legibilidade
        if (gameLevel == 1 && levelIndex == 0) {
            level.enemiesPerWave = 5;
            level.numberOfWaves = 2;
            level.timeBetweenWaves = 10;
            level.spawnInterval = 5;
        } else if (gameLevel == 1 && levelIndex == 1) {
            level.enemiesPerWave = 10;
            level.numberOfWaves = 2;
            level.timeBetweenWaves = 10;
            level.spawnInterval = 2;
        } else if (gameLevel == 1 && levelIndex == 2) {
            level.enemiesPerWave = 20;
            level.numberOfWaves = 2;
            level.timeBetweenWaves = 10;
            level.spawnInterval = 1;
        } else if (gameLevel == 1 && levelIndex == 3) {
            level.enemiesPerWave = 1;
            level.numberOfWaves = 1;
            level.timeBetweenWaves = 5;
            level.spawnInterval = .5f;
        } else if (gameLevel == 1 && levelIndex == 4) {
            level.enemiesPerWave = 25;
            level.numberOfWaves = 3;
            level.timeBetweenWaves = 5;
            level.spawnInterval = .5f;
        } else if (gameLevel == 2 && levelIndex == 0) {
            level.enemiesPerWave = 5;
            level.numberOfWaves = 2;
            level.timeBetweenWaves = 10;
            level.spawnInterval = 5;
        } else if (gameLevel == 2 && levelIndex == 1) {
            level.enemiesPerWave = 10;
            level.numberOfWaves = 2;
            level.timeBetweenWaves = 10;
            level.spawnInterval = 2;
        } else if (gameLevel == 2 && levelIndex == 2) {
            level.enemiesPerWave = 20;
            level.numberOfWaves = 2;
            level.timeBetweenWaves = 10;
            level.spawnInterval = 1;
        } else if (gameLevel == 2 && levelIndex == 3) {
            level.enemiesPerWave = 1;
            level.numberOfWaves = 1;
            level.timeBetweenWaves = 5;
            level.spawnInterval = .5f;
        } else if (gameLevel == 2 && levelIndex == 4) {
            level.enemiesPerWave = 30;
            level.numberOfWaves = 5;
            level.timeBetweenWaves = 5;
            level.spawnInterval = .2f;
        }  else if (gameLevel == 3 && levelIndex == 0) {
            level.enemiesPerWave = 5;
            level.numberOfWaves = 2;
            level.timeBetweenWaves = 10;
            level.spawnInterval = 5;
        } else if (gameLevel == 3 && levelIndex == 1) {
            level.enemiesPerWave = 10;
            level.numberOfWaves = 2;
            level.timeBetweenWaves = 10;
            level.spawnInterval = 2;
        } else if (gameLevel == 3 && levelIndex == 2) {
            level.enemiesPerWave = 20;
            level.numberOfWaves = 2;
            level.timeBetweenWaves = 10;
            level.spawnInterval = 1;
        } else if (gameLevel == 3 && levelIndex == 3) {
            level.enemiesPerWave = 1;
            level.numberOfWaves = 1;
            level.timeBetweenWaves = 5;
            level.spawnInterval = .5f;
        } else if (gameLevel == 3 && levelIndex == 4) {
            level.enemiesPerWave = 30;
            level.numberOfWaves = 5;
            level.timeBetweenWaves = 5;
            level.spawnInterval = .2f;
        }
    }

    private void NextSceneOrRestart() {
        int totalScenes = SceneManager.sceneCountInBuildSettings;
        if (SceneManager.GetActiveScene().buildIndex + 1 < totalScenes) {
            // Colocar a lógica de ADS aqui.


            StaticData.gemas = CoinManager.main.currentGemas;

            int currentEnemiesInBase = BuildManager.main.GetCurrentEnemiesInBase();
            StaticData.gemas_win = 4 - currentEnemiesInBase;
            // Zera desconto
            CoinManager.main.discountGemas = 0;

            Debug.Log("Gemas: " + CoinManager.main.currentGemas);
            Debug.Log("Desconto: " + CoinManager.main.discountGemas);
            Debug.Log("Fator: " + CoinManager.main.factorGemas);
            

            // Ainda há cenas para jogar, vai para a próxima cena
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        } else {
            SceneManager.LoadScene("Menu");
        }
    }

    public void RestartCurrentWave() {
        StopAllCoroutines();
        levelInProgress = false;
        StartCoroutine(StartNextLevel(levels[currentLevel], currentWave));
    }

    // Adicionado um método para reiniciar o nível atual sem incrementar o contador
    public void RestartWave() {
        StopAllCoroutines();
        levelInProgress = false;
        StartCoroutine(StartNextLevel(levels[currentLevel], currentWave));
    }
}
