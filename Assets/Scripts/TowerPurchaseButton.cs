using UnityEngine;
using UnityEngine.UI;

public class TowerPurchaseButton : MonoBehaviour
{
    public int towerCost; // Set this in the inspector for each tower's button
    private Button button;
    private Image buttonImage;
    private CoinManager coinManager;

    void Start()
    {
        button = GetComponent<Button>();
        buttonImage = GetComponent<Image>();
        coinManager = FindObjectOfType<CoinManager>(); // Finds the CoinManager in the scene
        UpdateButtonState();
    }

    void Update()
    {
        UpdateButtonState();
    }

    void UpdateButtonState()
    {
        if (coinManager.currentCoins >= towerCost)
        {
            button.interactable = true;
            buttonImage.color = new Color(buttonImage.color.r, buttonImage.color.g, buttonImage.color.b, 1f);
        }
        else
        {
            button.interactable = false;
            buttonImage.color = new Color(buttonImage.color.r, buttonImage.color.g, buttonImage.color.b, 0.5f);
        }
    }
}
