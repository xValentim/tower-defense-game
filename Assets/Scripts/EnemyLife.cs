using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100;
    private float currentHealth;
    public GameObject healthBarGreen; // The green bar, which should have a SpriteRenderer and be a child of a SpriteMask
    public bool simulateDamage = false; // Toggle this with UI checkbox

    private Vector3 healthBarOriginalPosition;
    private float originalHealthBarWidth;
    public int currentWorth = 50;

    void Start()
    {
        currentHealth = maxHealth;
        healthBarOriginalPosition = healthBarGreen.transform.localPosition;
        originalHealthBarWidth = healthBarGreen.GetComponent<SpriteRenderer>().bounds.size.x;

        if (simulateDamage)
        {
            InvokeRepeating("SimulateDamage", 2.0f, 2.0f); // Simulate damage every 2 seconds
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        UpdateHealthBar();

        if (currentHealth <= 0)
        {   
            Die();
        }
    }

    void UpdateHealthBar()
    {
        float healthRatio = currentHealth / maxHealth;
        // Update the position of the green health bar to simulate shrinking
        healthBarGreen.transform.localPosition = new Vector3(healthBarOriginalPosition.x - (1 - healthRatio) * originalHealthBarWidth / 2, healthBarOriginalPosition.y, healthBarOriginalPosition.z);
        healthBarGreen.transform.localScale = new Vector3(healthRatio, 1f, 1f);
    }

    void Die()
    {
        // Add coins to the player's inventory when the enemy dies
        LevelManager.main.IncreaseCoins(currentWorth);
        // Handle enemy death here (e.g., play animation, remove from game)
        Debug.Log("Acabou");
        Destroy(gameObject);
    }

    public void ToggleDamageSimulation(bool toggle)
    {
        simulateDamage = toggle;
        if (simulateDamage)
        {
            InvokeRepeating("SimulateDamage", 0f, 2.0f); // Start simulating damage immediately
        }
        else
        {
            CancelInvoke("SimulateDamage");
        }
    }

    void SimulateDamage()
    {
        TakeDamage(20); // Simulate taking 10 points of damage
    }
}

