using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShopScript : MonoBehaviour
{
    [System.Serializable]
    public class StoreItem
    {
        public string name;
        public int price;
    }

    public GameObject storeItemPrefab;
    public GameObject[] mutes;
    public Animator shopAnimator;

    public StoreItem[] itemsForSale;
    private GameObject inventory;

    // Start is called before the first frame update
    void Start()
    {
        
        foreach(StoreItem si in itemsForSale)
        {
            GameObject go = Instantiate(storeItemPrefab);
            go.transform.SetParent(transform, false);
        }
        CloseShop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenShop()
    {
        foreach (GameObject item in mutes)
        {
         //   item.SetActive(false);
        }
        shopAnimator.SetTrigger("OpenShop");
    }

    public void CloseShop()
    {
      // StartCoroutine(animateCloseShop());

        shopAnimator.SetTrigger("CloseShop");

        foreach (GameObject item in mutes)
        {
        //    item.SetActive(true);
        }
    }

}
