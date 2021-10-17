using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI muteText;
    void Start()
    {
        updateSoundControls();
    }

    public static void QuitGame()
    {
        Application.Quit();
    }

    public void ToggleMute()
    {
        GameManagerScript.isMuted = !GameManagerScript.isMuted;
        updateSoundControls();
    }

    private void updateSoundControls()
    {
        muteText.text = GameManagerScript.isMuted ? "X" : "";
        GameObject.Find("Player").GetComponent<AudioSource>().enabled = !GameManagerScript.isMuted;
    }

    public void B()
    {

    }

    public void Play()
    {
        GameManagerScript.isGameStarted = true;
    }
}
