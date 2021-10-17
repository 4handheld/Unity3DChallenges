using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{

   
    public float chanceToSpawn = 0.5f;

    private GameObject[] coins;


    private void Awake()
    {
        coins = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            coins[i] = transform.GetChild(i).gameObject;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        if (Random.Range(0, 1) > chanceToSpawn)
        {
            return;
        }
        int r = Random.Range(0, coins.Length);
        for (int i = 0; i < r; i++)
        {
         //   coins[i].SetActive(true);
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < coins.Length; i++)
        {
          //  coins[i].SetActive(true);
        }
    }
}
