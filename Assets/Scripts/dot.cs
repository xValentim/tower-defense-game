using UnityEngine;

public class dot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;

    private GameObject tower;
    private Color startColor;

    private void Start()
    {
        startColor = sr.color;
    }

    private void OnMouseEnter()
    {
        sr.color = hoverColor;
    }

    private void OnMouseExit()
    {
        sr.color = startColor;
    }

    private void OnMouseDown()
    {
        CoinManager coinManager = FindObjectOfType<CoinManager>(); // Encontra o CoinManager na cena

        if (BuildManager.main.isDestroyMode && tower != null)
        {
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
        if (coinManager.currentCoins >= towerToBuild.cost)
        {
            tower = Instantiate(towerToBuild.prefab, newPosition, Quaternion.identity);

            // Se estiver de buff, aumenta o alcance da torre
            if (coinManager.buff == true)
            {
                Debug.Log("Variável buff é: " + coinManager.buff);
                tower.GetComponent<Tower_Default>().targetingRange *= 2; // Aumenta só targetingRange (100%)
                Debug.Log("Buff targetingRange ativado! Alcance da torre aumentado para: " + tower.GetComponent<Tower_Default>().targetingRange);
            }

            // Se estiver de buff2, aumenta fireRate da torre
            if (coinManager.buff2 == true)
            {
                Debug.Log("Variável buff é: " + coinManager.buff2);
                tower.GetComponent<Tower_Default>().fireRate *= 2; // Aumenta só fireRate (100%)
                Debug.Log("Buff fireRate ativado! Alcance da torre aumentado para: " + tower.GetComponent<Tower_Default>().fireRate);
            }

            Debug.Log("Torre criada em: " + newPosition);

            // Remove as moedas do jogador
            coinManager.RemoveCoins(towerToBuild.cost);

            // Faz com que o dot desapareça após a torre ser construída, mas ainda seja clicável
            sr.enabled = false;
        }
        else
        {
            Debug.Log("Você não tem moedas suficientes para comprar esta torre!");
        }
    }

    public void PlaceTower(GameObject towerPrefab, int cost)
    {
        CoinManager coinManager = FindObjectOfType<CoinManager>(); // Encontra o CoinManager na cena

        if (tower != null)
        {
            Debug.Log("PlaceTower: Já existe uma torre aqui.");
            return;
        }

        if (coinManager.currentCoins < cost)
        {
            Debug.Log("PlaceTower: Moedas insuficientes.");
            return;
        }

        Vector3 newPosition = transform.position; // Posição do dot
        // Adicione um deslocamento adicional à posição da torre, se necessário
        newPosition += new Vector3(0.1f, 0.3f, 0);

        tower = Instantiate(towerPrefab, newPosition, Quaternion.identity);

        // Lógica de buff
        if (coinManager.buff == true)
        {
            Debug.Log("Variável buff é: " + coinManager.buff);
            tower.GetComponent<Tower_Default>().targetingRange *= 2; // Aumenta só targetingRange (100%)
            Debug.Log("Buff targetingRange ativado! Alcance da torre aumentado para: " + tower.GetComponent<Tower_Default>().targetingRange);
        }

        if (coinManager.buff2 == true)
        {
            Debug.Log("Variável buff2 é: " + coinManager.buff2);
            tower.GetComponent<Tower_Default>().fireRate *= 2; // Aumenta só fireRate (100%)
            Debug.Log("Buff fireRate ativado! Fire rate da torre aumentado para: " + tower.GetComponent<Tower_Default>().fireRate);
        }

        Debug.Log("Torre criada em: " + newPosition);

        // Remove as moedas do jogador
        coinManager.RemoveCoins(cost);

        // Faz com que o dot desapareça após a torre ser construída, mas ainda seja clicável
        sr.enabled = false;
    }
}
