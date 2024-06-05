using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GemasCount : MonoBehaviour
{
    public CoinManager coinManager; // Referência ao CoinManager

    public TextMeshProUGUI gemasText; // Referência ao texto de gemas


    void Start()
    {
        // Encontre o CoinManager na cena
        coinManager = FindObjectOfType<CoinManager>();
        
        // Atualize o texto das moedas no início
        UpdateGemasUI();
    }

    void Update()
    {
        // Atualize o texto das moedas a cada quadro
        UpdateGemasUI();
    }

    void UpdateGemasUI()
    {
        // Verifique se o CoinManager foi encontrado na cena
        if (coinManager != null)
        {
            gemasText.text = coinManager.currentGemas.ToString();
        }
    }
}
