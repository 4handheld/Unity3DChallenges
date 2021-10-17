using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelixManager : MonoBehaviour
{

    public GameObject[] helixRings;
    public GameObject lastRing;
    public GameObject startRing;
    public float ySpawn = 0;
    public float ringDistance = 5;

    public int numOfRings;
    public GameObject pillerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        numOfRings = GameManagerScript.currentLevelIndex + 5;

        spawnStartRing();
        for (int i = 0; i < numOfRings; i++)
        {
            spawnRing(Random.Range(0,helixRings.Length));
        }
        spawnLastRing();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnRing(int ringIndex)
    {
        GameObject cylinder = Instantiate(pillerPrefab, transform.up * ySpawn, Quaternion.identity);
        GameObject ring = Instantiate(helixRings[ringIndex],transform.up * ySpawn, Quaternion.identity);
        ring.transform.parent =transform;
        ySpawn -= ringDistance;
    }

    public void spawnLastRing()
    {
        GameObject cylinder = Instantiate(pillerPrefab, transform.up * ySpawn, Quaternion.identity);
        GameObject ring = Instantiate(lastRing, transform.up * ySpawn, Quaternion.identity);
        ring.transform.parent = transform;
        ySpawn -= ringDistance;
    }

    public void spawnStartRing()
    {
        GameObject cylinder = Instantiate(pillerPrefab, transform.up * ySpawn, Quaternion.identity);
        GameObject ring = Instantiate(startRing, transform.up * ySpawn, Quaternion.identity);
        ring.transform.parent = transform;
        ySpawn -= ringDistance;
    }
}
