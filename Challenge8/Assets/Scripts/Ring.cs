using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    private const float VolumeScale = 1;
    private Transform playerTransform;
    public AudioClip whooshClip;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y > playerTransform.position.y)
        {
            playerTransform.gameObject.GetComponent<AudioSource>().PlayOneShot(whooshClip, VolumeScale);
            GameManagerScript.numOfPassedRIngs++;
            GameManagerScript.score++;
            Destroy(gameObject);
        }
    }
}
