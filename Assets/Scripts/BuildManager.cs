using UnityEngine;
using UnityEngine.SceneManagement;


public class BuildManager : MonoBehaviour {
    public static BuildManager main;

    [Header("References")]
    [SerializeField] private Tower[] towers;
    private int selectedTower = 0;
    public bool isDestroyMode = false;

    [Header("Enemy Control")]
    public int maxEnemiesInBase = 2;
    private int currentEnemiesInBase = 0;

    private void Awake() {
        main = this;
    }

    public Tower GetSelectedTower() {
        return towers[selectedTower];
    }

    public void SelectTower(int _selectedTower) {
        selectedTower = _selectedTower;
        isDestroyMode = false;  // Desativa o modo de destruição ao selecionar uma torre
    }

    public void ToggleDestroyMode() {
        isDestroyMode = !isDestroyMode;  // Alterna o estado do modo de destruição
    }

    public void ToggleBackToMenu() {
        SceneManager.LoadScene("Menu");
          // Alterna o estado do modo de destruição
    }

    public void EnemyReachedBase() {
        currentEnemiesInBase++;
        UIManager.instance.UpdateEnemyCount(currentEnemiesInBase); // Atualiza o UI

        if (currentEnemiesInBase >= maxEnemiesInBase) {
            Debug.Log("Chamando endgame");  // Isso mostrará o total no console

            EndGame();
        }
    }

    public void EndGame() {
        Debug.Log("Game Over! Too many enemies have reached the base.");
        // Aqui você pode adicionar qualquer lógica adicional para quando o jogo terminar,
        // como pausar o jogo, mostrar uma tela de Game Over, etc.

        // Por enquanto, vamos recarregar a cena atual
        SceneManager.LoadScene("Menu");
    }
}
