using UnityEngine;
using UnityEngine.UI;

public class TowerUpgradeButton : MonoBehaviour
{
    public int towerUpgradeCost; 
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
        if (coinManager.currentGemas >= towerUpgradeCost)
        {
            button.interactable = true;
            buttonImage.color = new Color(buttonImage.color.r, buttonImage.color.g, buttonImage.color.b, 1f);
        }
        else
        {
            button.interactable = false;
            buttonImage.color = new Color(buttonImage.color.r, buttonImage.color.g, buttonImage.color.b, 0.5f);
        }

        if (towerUpgradeCost == 4 && coinManager.buff)
        {
            button.interactable = false;
            buttonImage.color = new Color(buttonImage.color.r, buttonImage.color.g, buttonImage.color.b, 0.5f);
        }

        if (towerUpgradeCost == 8 && coinManager.buff2)
        {
            button.interactable = false;
            buttonImage.color = new Color(buttonImage.color.r, buttonImage.color.g, buttonImage.color.b, 0.5f);
        }
    }
}
