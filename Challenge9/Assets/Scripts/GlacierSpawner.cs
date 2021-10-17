using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlacierSpawner : MonoBehaviour
{

    public float DISTANCE_TO_RESPAWN = 1.0f;
    public float scrollSpeed = -2.0f;
    public float totalLength = 2;

    private float scrollPosition;
    private Transform playerTransform;


    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManagerScript.isGameStarted)
        {
            return;
        }

        scrollPosition += scrollSpeed * Time.deltaTime;
        Vector3 neLocation = (playerTransform.position.z + scrollPosition) * Vector3.forward;
        transform.position = neLocation;
        if(transform.GetChild(0).transform.position.z < playerTransform.position.z - DISTANCE_TO_RESPAWN)
        {
            transform.GetChild(0).localPosition += Vector3.forward * totalLength;
            transform.GetChild(0).SetSiblingIndex(transform.childCount);
            
            transform.GetChild(0).localPosition += Vector3.forward * totalLength;
            transform.GetChild(0).SetSiblingIndex(transform.childCount);
        }
    }
}
