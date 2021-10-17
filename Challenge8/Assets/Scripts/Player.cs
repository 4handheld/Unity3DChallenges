using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player: MonoBehaviour
{

    private Rigidbody rigidBody;
    private AudioSource audioSource;
    public AudioClip bounceClip,gameOverClip, winClip;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        audioSource.PlayOneShot(bounceClip,0.5f);
        rigidBody.velocity = new Vector3(0, 6, 0);
        string materialName = collision.transform.GetComponent<MeshRenderer>().material.name;
        Debug.Log(materialName);
        if(materialName == "Green (Instance)")
        {

        }else if (materialName == "Red (Instance)")
        {
            GameManagerScript.gameOver = true;
            audioSource.PlayOneShot(gameOverClip, 0.5f);
        }
        else if (materialName == "Black (Instance)")
        {
            GameManagerScript.levelCompleted = true;
            audioSource.PlayOneShot(winClip, 0.5f);
        }
    }
}
