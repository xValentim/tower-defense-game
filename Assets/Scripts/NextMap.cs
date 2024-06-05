using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextMap : MonoBehaviour
{
    [Header("Ad Control")]
    [SerializeField] private InterstitialAd interstitialAd;

    private void Start() {
        interstitialAd.LoadAd();
        interstitialAd.OnAdCompleted += AdWatchCompleted;
    }

    private void OnDestroy() {
        interstitialAd.OnAdCompleted -= AdWatchCompleted;
    }

    // Método para avançar para a próxima fase
    public void LoadNextLevel()
    {
        StaticData.gemas = StaticData.gemas + StaticData.gemas_win;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Método para assistir ao anúncio
    public void AdWatch()
    {
        interstitialAd.ShowAd();
    }

    // Método chamado quando o anúncio é assistido
    private void AdWatchCompleted()
    {
        // Incrementa o valor das gemas com base no resultado do anúncio
        StaticData.gemas += StaticData.gemas_win;
        LoadNextLevel();
    }

    // Método para voltar ao menu principal
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
