using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GoogleMobileAds.Api;

public class GameAdManager : MonoBehaviour
{

    private BannerView bannerAD;
    private InterstitialAd interstitialAd;
    public static GameAdManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        MobileAds.Initialize(InitializationStatus => { });
        this.reuestBannerAd();
    }

    private void reuestBannerAd()
    {

        string appId = "ca-app-pub-3940256099942544~3347511713";
        string adUnit = "ca-app-pub-3940256099942544/6300978111";
        this.bannerAD = new BannerView(adUnit, AdSize.SmartBanner, AdPosition.Bottom);
        AdRequest adr = new AdRequest.Builder().Build();
        this.bannerAD.LoadAd(adr);
    }

    public void reuestInterstitialAd()
    {
        if (this.interstitialAd != null)
        {
            this.interstitialAd.Destroy();
        }

        string appId = "ca-app-pub-3940256099942544~3347511713";
        string adUnit = "ca-app-pub-3940256099942544/6300978111";
        this.interstitialAd = new InterstitialAd(adUnit);
        AdRequest adr = new AdRequest.Builder().Build();
        this.interstitialAd.LoadAd(adr);
    }

    public void shoInterstitialAd()
    {
        if (this.interstitialAd.IsLoaded())
        {
            this.interstitialAd.Show();
        }
        else
        {
            Debug.Log("Interstital is not ready yet.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
