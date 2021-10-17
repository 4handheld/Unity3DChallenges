using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MusicScript : MonoBehaviour
{
    // Start is called before the first frame update

    public static bool on;
    public TextMeshProUGUI text;
    public AudioSource[] audioSources;

    private void Start()
    {
        text.text = on ? "Music On" : "Music Off";
        foreach (AudioSource item in audioSources)
        {
            item.enabled = on;
        }
    }

    public void flip()
    {
        on = !on;
        text.text = on ? "Music On" : "Music Off";
        foreach (AudioSource item in audioSources)
        {
            item.enabled = on;
        }
    }

}
