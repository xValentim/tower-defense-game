using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour

{
    public AudioSource clickSound;
    public GameObject settingsMenu;

    void Start() 
    {
        settingsMenu.SetActive(false); // Inicia o jogo com o SettingsMenu invisível
    }
    public void PlayGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame(){
        Application.Quit();
    }

    public void PlayClickSound() {
        clickSound.Play();
    }

}