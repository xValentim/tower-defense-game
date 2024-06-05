using UnityEngine;

public class AdPopup : MonoBehaviour {
    public GameObject popupUI;

    public void ShowPopup() {
        popupUI.SetActive(true);
    }

    public void HidePopup() {
        popupUI.SetActive(false);
    }

    public void OnWatchAdButton() {
        BuildManager.main.WatchAdToContinue();
        HidePopup();
    }

    public void OnEndGameButton() {
        BuildManager.main.EndGame();
        HidePopup();
    }
}
