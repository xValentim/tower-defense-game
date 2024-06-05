using UnityEngine;
using UnityEngine.SceneManagement;

public class BuildManager : MonoBehaviour {
    public static BuildManager main;

    [Header("References")]
    [SerializeField] private Tower[] towers;
    private int selectedTower = 0;
    public bool isDestroyMode = false;

    public GameObject CoinsManager;

    [Header("Enemy Control")]
    public int maxEnemiesInBase = 2;
    private int currentEnemiesInBase = 0;

    [Header("Ad Control")]
    [SerializeField] private InterstitialAd interstitialAd;
    [SerializeField] private AdPopup adPopup;

    [Header("Wave Manager")]
    [SerializeField] private WaveManager waveManager;
    
    private bool adWatched = false; // Sinalizador para verificar se o anúncio foi assistido
    private int defeatCount = 0; // Contador de derrotas

    private void Awake() {
        main = this;
    }

    private void Start() {
        interstitialAd.LoadAd();
    }

    public Tower GetSelectedTower() {
        return towers[selectedTower];
    }

    public void SelectTower(int _selectedTower) {
        selectedTower = _selectedTower;
        isDestroyMode = false;
    }

    public void ToggleDestroyMode() {
        isDestroyMode = !isDestroyMode;
    }

    public void ToggleBackToMenu() {
        SceneManager.LoadScene("Menu");
    }

    public void EnemyReachedBase() {
        currentEnemiesInBase++;
        UIManager.instance.UpdateEnemyCount(currentEnemiesInBase);

        if (currentEnemiesInBase >= maxEnemiesInBase) {
            HandleDefeat();
        }
    }

    private void HandleDefeat() {
        defeatCount++;
        if (defeatCount == 1) { // Primeira derrota
            if (!adWatched) { // Verifica se o anúncio já foi assistido
                ShowAdPopup();
            } else {
                EndGame();
            }
        } else { // Segunda derrota
            EndGame();
        }
    }

    public void ShowAdPopup() {
        if (adPopup != null) {
            DestroyAllEnemies();
            adPopup.ShowPopup();
        } else {
            Debug.LogError("AdPopup reference is not set in the BuildManager.");
        }
    }

    private void DestroyAllEnemies() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies) {
            Destroy(enemy);
        }
    }

    public void WatchAdToContinue() {
        interstitialAd.ShowAd();
    }

    public void ResetEnemyCount() {
        currentEnemiesInBase = 0;
        UIManager.instance.UpdateEnemyCount(currentEnemiesInBase);
    }

    public void EndGame() {
        Debug.Log("Game Over! Too many enemies have reached the base.");
        SceneManager.LoadScene("Menu");
    }

    public void OnAdWatched() {
        adWatched = true; // Define o sinalizador como verdadeiro quando o anúncio é assistido
        ResetEnemyCount();
        waveManager.RestartWave(); // Reinicia a wave atual
    }

    // Método para verificar se o anúncio foi assistido
    public bool IsAdWatched() {
        return adWatched;
    }

    // Método para obter o valor de currentEnemiesInBase
    public int GetCurrentEnemiesInBase() {
        return currentEnemiesInBase;
    }
}
