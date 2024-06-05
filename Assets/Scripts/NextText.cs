using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NextText : MonoBehaviour
{
    public static NextText instance;

    [SerializeField] private TextMeshProUGUI winText; // Se estiver usando TextMeshPro

    // Start and update the text
    private void Start() {
        winText.text = "Parabéns, você obteve: " + StaticData.gemas_win.ToString() + " gemas!\nDeseja assistir um anúncio para dobrar suas gemas?";
    }
}
