using UnityEngine;

public class dot : MonoBehaviour {
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;

    private GameObject tower;
    private Color startColor;

    private void Start() {
        startColor = sr.color;
    }

    private void OnMouseEnter() {
        sr.color = hoverColor;
    }

    private void OnMouseExit() {
        sr.color = startColor;
    }

    private void OnMouseDown() {
        CoinManager coinManager = FindObjectOfType<CoinManager>(); // Encontra o CoinsManager na cena

        if (BuildManager.main.isDestroyMode && tower != null) {
            Destroy(tower);
            tower = null;
            sr.enabled = true;
            Debug.Log("Torre destruída.");
            return;
        }

        if (tower != null || BuildManager.main.isDestroyMode) return;

        Tower towerToBuild = BuildManager.main.GetSelectedTower();
        Vector3 newPosition = transform.position; // Posição do dot
        // Adicione um deslocamento adicional à posição da torre, se necessário
        newPosition += new Vector3(0.1f, 0.3f, 0);
        
        // Verifica se o jogador tem moedas suficientes para comprar a torre
        if (coinManager.currentCoins >= towerToBuild.cost) {
            tower = Instantiate(towerToBuild.prefab, newPosition, Quaternion.identity);
            Debug.Log("Torre criada em: " + newPosition);
            
            // Remove as moedas do jogador
            coinManager.RemoveCoins(towerToBuild.cost);

            // Faz com que o dot desapareça após a torre ser construída, mas ainda seja clicável
            sr.enabled = false;

        } else {
            Debug.Log("Você não tem moedas suficientes para comprar esta torre!");
        }
    }
}
