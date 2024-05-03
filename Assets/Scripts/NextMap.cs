using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextMap : MonoBehaviour
{
    // Método para avançar para a próxima fase
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Método para voltar ao menu principal
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
