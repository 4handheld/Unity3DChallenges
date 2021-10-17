using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyRewards : MonoBehaviour
{

    public GameObject dailyReward;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        float now = System.DateTime.Now.Day;
        PlayerPrefs.SetFloat("LAST_TIME_SEEN", now);
        float lastStartTime = PlayerPrefs.GetFloat("LAST_TIME_SEEN", -1);
        Debug.Log("lastStartTime : " + lastStartTime);
        Debug.Log("now : " + now);
        if (now != lastStartTime)
        {
            ShowDailyReward();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowDailyReward()
    {
        GameManagerScript.totalCoins += 10;
        anim.SetTrigger("Show");
    }

    public void CloseDailyReward()
    {
        anim.SetTrigger("Hide");
    }
}
