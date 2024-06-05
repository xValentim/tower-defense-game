using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinsCount : MonoBehaviour
{
    public CoinManager coinManager; // Referência ao CoinManager

    public TextMeshProUGUI coinsText; // Referência ao texto de moeda


    void Start()
    {
        // Encontre o CoinManager na cena
        coinManager = FindObjectOfType<CoinManager>();
        
        // Atualize o texto das moedas no início
        UpdateCoinsUI();
    }

    void Update()
    {
        // Atualize o texto das moedas a cada quadro
        UpdateCoinsUI();
    }

    void UpdateCoinsUI()
    {
        // Verifique se o CoinManager foi encontrado na cena
        if (coinManager != null)
        {
            // Atualize o texto com o valor das moedas atuais do CoinManager
            coinsText.text = coinManager.currentCoins.ToString();
        }
    }
}
