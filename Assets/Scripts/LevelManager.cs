using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;
    public Waypoint startPoint;

    public CoinManager coinManager;

    private void Awake()
    {
        main = this;
    }

    public void IncreaseCoins(int amount)
    {
        coinManager.AddCoins(amount);
    }

    public void SpendCoins(int amount)
    {
        coinManager.BuyTower(amount);
    }

    public void IncreaseGemas(int amount)
    {
        coinManager.AddGemas(amount);
    }

    public void SpendGemas(int amount)
    {
        coinManager.RemoveGemas(amount);
    }

    public void NextLevelScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
}
