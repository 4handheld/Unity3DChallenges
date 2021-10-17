using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using GoogleMobileAds.Api;

public class GameManagerScript : MonoBehaviour
{

    public static GameManagerScript instance;
    public static bool isGameStarted = false;
    private PlayerMotor playerMotor;

    // UI and UI fields
    public Animator gameCanvas;
    public Animator IntroMenu;
    public TextMeshProUGUI scoreTextUI, coinTextUI, modifierTextUI;
    public float score, coinScore, modifierScore;

    private string scorePrefix, coinPrefix, modifierPrefix;

    //Death Menu
    public Animator deathMenuAnim;
    public TextMeshProUGUI GameOverscoreText, GameOverHighscoreText, GameOvercoinsText;

    public float highScore;

    public static List<string> saveContents = new List<string>();

    public static float totalCoins;
    private InterstitialAd interstitialAd;

    private void Awake()
    {
        instance = this;
        scorePrefix = scoreTextUI.text;
        coinPrefix = coinTextUI.text;
        modifierPrefix = modifierTextUI.text;
        modifierScore = 1;
        highScore = PlayerPrefs.GetFloat("PLAYER_HIGH_SCORE", 0);
        totalCoins = PlayerPrefs.GetFloat("PLAYER_TOTAL_COINS", 0);
        updateScores();
        MobileAds.Initialize(InitializationStatus => { });
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

    public void updateScores()
    {
        scoreTextUI.text = scorePrefix +" : "+ score.ToString("0");
        coinTextUI.text = coinPrefix + " : " + coinScore.ToString("0");
        modifierTextUI.text = modifierPrefix + " : x" + modifierScore.ToString("0.0");
    }

    // Start is called before the first frame update
    void Start()
    {
    
    }


    public void startGame()
    {
        GameObject.Find("Player").GetComponent<Animator>().SetTrigger("StartRunning");
        isGameStarted = true;
        gameCanvas.SetTrigger("Show");
        IntroMenu.SetTrigger("Hide");
    }

    // Update is called once per frame
    void Update()
    {
        if (MobileInput.instance.Tap && !isGameStarted && false)
        {
            return;
            GameObject.Find("Player").GetComponent<Animator>().SetTrigger("StartRunning");
            isGameStarted = true;
            gameCanvas.SetTrigger("Show");
            IntroMenu.SetTrigger("Hide");
        }
        if (isGameStarted)
        {
            score += (Time.deltaTime * modifierScore);
            scoreTextUI.text = scorePrefix + " : " + score.ToString("0");
        }
        coinTextUI.text = coinPrefix + " : " + coinScore.ToString("0");
    }

    public void addCoin()
    {
        coinScore += 5;
        switch (coinScore)
        {
            case 50:
                PlayGameServicesScript.instance.UnlockAchievement("");
                break;
            case 100:
                PlayGameServicesScript.instance.UnlockAchievement("");
                break;
            case 150:
                PlayGameServicesScript.instance.UnlockAchievement("");
                break;
            case 200:
                PlayGameServicesScript.instance.UnlockAchievement("");
                break;

        }
    }

    public void OnPlayButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }

    public void OnDeath()
    {
        isGameStarted = false;
       
        GameOverscoreText.text = "Your Score : "+score.ToString("0");
        GameOvercoinsText.text = "Coins : "+coinScore.ToString("0");
        GameOverHighscoreText.text = "High Score : "+ highScore.ToString("0");
        deathMenuAnim.SetTrigger("Death");
        gameCanvas.SetTrigger("Hide");

        totalCoins += coinScore;
        PlayerPrefs.SetFloat("PLAYER_TOTAL_COINS", totalCoins);

        //CHeck HighScore
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetFloat("PLAYER_HIGH_SCORE", highScore);
            PlayGameServicesScript.instance.ReportScore( (int)highScore,"");
        }
        Invoke("openInterstitial", 2f);
    }

    public void openInterstitial()
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




}
