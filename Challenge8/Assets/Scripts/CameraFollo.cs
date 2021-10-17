using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollo : MonoBehaviour
{

    private GameObject player;
    private Vector3 offset;
    public float smoothSpeed = 0.04f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 position = Vector3.Lerp(transform.position, player.transform.position + offset, smoothSpeed);
        transform.position = position;

    }
}
