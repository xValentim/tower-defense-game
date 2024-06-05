using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public static CoinManager main;
    public int startingCoins = 100; // Moedas iniciais do jogador
    public int startingGemas = 0; // Gemas iniciais do jogador
    public int factorGemas = 1; // Fator multiplicativo para gemas
    public int discountGemas = 0; // Desconto para gemas
    public int currentCoins; // Moedas atuais do jogador
    public int currentGemas; // Gemas atuais do jogador
    public bool buff = false;
    private int amountBuff1 = 4;
    public bool buff2 = false;
    private int amountBuff2 = 8;
    

    public Text coinsText;
    public Text gemasText;
    [SerializeField] private AudioClip buyTowerSound;
    [SerializeField] private AudioClip deadEnemySound;
    private AudioSource audioSourceTower;
    private AudioSource audioSourceEnemy;

    private void Awake() {
        main = this;
    }

    void Start()
    {
        currentCoins = startingCoins;
        currentGemas = StaticData.gemas;
        buff = StaticData.buff;
        buff2 = StaticData.buff2;

        audioSourceTower = GetComponent<AudioSource>();
        audioSourceEnemy = GetComponent<AudioSource>();

        // UpdateCoinsUI(); 
    }

    
    void UpdateCoinsUI()
    {
        coinsText.text = "Moedas: " + currentCoins.ToString();

    }

    void UpdateGemasUI()
    {
        gemasText.text = "Gemas: " + currentGemas.ToString();
    }

    public void AddCoins(int amount)
    {
        currentCoins += amount;
        audioSourceTower.clip = deadEnemySound;
        audioSourceEnemy.Play();
        // UpdateCoinsUI();
    }

    public void addDiscountGemas(int amount)
    {
        discountGemas += amount;
        Debug.Log("Desconto: " + discountGemas);
    }

    public void AddGemas(int amount)
    {
        currentGemas += (amount * factorGemas) - discountGemas;
        StaticData.gemas = currentGemas;
        Debug.Log("Gemas: " + currentGemas);
    }

    public void RemoveCoins(int amount)
    {
        if (currentCoins >= amount)
        {
            currentCoins -= amount;
            audioSourceTower.clip = buyTowerSound;
            audioSourceTower.Play();
        }
        else
        {
            Debug.Log("Não há moedas suficientes!");
            
        }
    }

    public void RemoveGemas(int amount)
    {
        if (currentGemas >= amount)
        {
            if (amount == amountBuff1 && !buff)
            {
                currentGemas -= amount;
                Debug.Log("Gemas removidas: " + amount);
                Debug.Log("Gemas restantes: " + currentGemas);
                Debug.Log("Buff 1 ativado!");
                buff = true;
                StaticData.buff = buff;
            } else if (amount == amountBuff2 && !buff2)
            {
                currentGemas -= amount;
                Debug.Log("Gemas removidas: " + amount);
                Debug.Log("Gemas restantes: " + currentGemas);
                Debug.Log("Buff 2 ativado!");
                buff2 = true;
                StaticData.buff2 = buff2;
            } else
            {
                Debug.Log("Buff já ativado!");
            }
            StaticData.gemas = currentGemas;


        }
        else
        {
            Debug.Log("Não há gemas suficientes!");
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
