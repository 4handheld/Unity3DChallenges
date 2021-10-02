using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public GameObject dogPrefab;
	public float delayResetValue = 3;
	public float delay;
	public float delayRate = 1;

    // Update is called once per frame
    void Update()
    {
	delay -= Time.deltaTime*delayRate;

        // On spacebar press, send dog
        if (Input.GetKeyDown(KeyCode.Space) && delay<=0 )
        {
            Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);
		delay = delayResetValue;
        }
    }
}
