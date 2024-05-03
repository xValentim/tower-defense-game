using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Use esta linha se estiver usando TextMeshPro

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private TextMeshProUGUI enemiesText; // Se estiver usando TextMeshPro

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    public void UpdateEnemyCount(int count) {
        enemiesText.text = "Inimigos na casa: " + count;
    }
}

