using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotationSpeed = 150;
 
    void Update()
    {
        if (!GameManagerScript.isGameStarted)
        {
            return;
        }
        if (Input.GetMouseButton(0))
        {
            float mouseX = Input.GetAxisRaw("Mouse X");
            transform.Rotate(new Vector3(0, -mouseX * rotationSpeed * Time.deltaTime, 0));
        }

        if (Input.touchCount>0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            float mouseX = Input.GetTouch(0).deltaPosition.x;
            transform.Rotate(new Vector3(0, -mouseX * rotationSpeed * Time.deltaTime, 0));
        }
    }
}
