using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{

    private GameObject player;
    private Vector3 offset;
    private Vector3 rotation = new Vector3(7, 0,0);

    public bool IsMoving { get; set; }


    // Start is called before the first frame update
    void Start()
    {


        player = GameObject.Find("Player");
        offset = transform.position - player.transform.position;
        offset = new Vector3(0, 5, -10f) ;

      //  transform.position = player.transform.position + offset;
    }

    // Update is called once per frame
    void LateUpdate()
    {

        if (!GameManagerScript.isGameStarted)
        {
            return;
        }

        Vector3 desiredPosition = player.transform.position + offset;
        desiredPosition.x = 0;
        transform.position = Vector3.Lerp(transform.position, desiredPosition , 100 * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(rotation),2f*Time.deltaTime);

    }
}
