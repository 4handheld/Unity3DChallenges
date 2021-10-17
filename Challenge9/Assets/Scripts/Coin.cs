using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    
    public AudioSource audioSource;
    public AudioClip touchClip;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (MusicScript.on)
            {
                audioSource.PlayOneShot(touchClip);
            }
            
            GetComponent<Animator>().SetTrigger("Collected");
            GameManagerScript.instance.addCoin();
            Destroy(this.transform.parent.gameObject, 1.5f);
        }
        
    }

}
