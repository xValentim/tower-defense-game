using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public int startingCoins = 100; // Moedas iniciais do jogador
    public int currentCoins; // Moedas atuais do jogador

    public Text coinsText; 
    [SerializeField] private AudioClip buyTowerSound;
    [SerializeField] private AudioClip deadEnemySound;
    private AudioSource audioSourceTower;
    private AudioSource audioSourceEnemy;

    void Start()
    {
        currentCoins = startingCoins;
        audioSourceTower = GetComponent<AudioSource>();
        audioSourceEnemy = GetComponent<AudioSource>();
        // UpdateCoinsUI(); 
    }

    
    void UpdateCoinsUI()
    {
        coinsText.text = "Moedas: " + currentCoins.ToString();
    }

    public void AddCoins(int amount)
    {
        currentCoins += amount;
        audioSourceTower.clip = deadEnemySound;
        audioSourceEnemy.Play();
        // UpdateCoinsUI();
    }

    public void RemoveCoins(int amount)
    {
        if (currentCoins >= amount)
        {
            currentCoins -= amount;
            audioSourceTower.clip = buyTowerSound;
            audioSourceTower.Play();
            // UpdateCoinsUI();
        }
        else
        {
            Debug.Log("Não há moedas suficientes!");
            
        }
    }

    public void BuyTower(int towerCost)
    {
        if (currentCoins >= towerCost)
        {
            RemoveCoins(towerCost);
            audioSourceTower.clip = buyTowerSound;
            audioSourceTower.Play();
            Debug.Log("Torre comprada!");
        }
        else
        {
            Debug.Log("Você não tem moedas suficientes para comprar esta torre!");
        }
    }
}
