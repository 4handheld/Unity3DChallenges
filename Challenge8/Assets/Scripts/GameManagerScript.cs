using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameManagerScript : MonoBehaviour
{
    private const string SceneName = "Level";
    public static bool gameOver;
    public static bool levelCompleted;
    public static bool isGameStarted;
    public static bool isMuted;

    public GameObject gameOverPanel;
    public GameObject levelCompletedPanel;
    public GameObject gamePlayPanel;
    public GameObject startMenuPanel;

    public static int currentLevelIndex;
    public TextMeshProUGUI currentLevelText;
    public TextMeshProUGUI nextLevelText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public Slider progressSlider;

    private static string PlayerPrefLevelKey = "CurrentLevelIndex";
    private static string GamePrefBestScoreKey = "BestScore";

    public static int numOfPassedRIngs;
    public static int score = 0;
    public static int highscore = 0;

    public static string SceneName1 => SceneName;

    private void Awake()
    {
        currentLevelIndex = PlayerPrefs.GetInt(PlayerPrefLevelKey,1);
    }

    // Start is called before the first frame update
    void Start()
    {
        isGameStarted = isMuted = gameOver = levelCompleted = false;
        highscore = PlayerPrefs.GetInt(GamePrefBestScoreKey, 0);
        highScoreText.text = "Best Score\n"+ highscore.ToString();
        Time.timeScale = 1;
        numOfPassedRIngs = 0;
        GameAdManager.instance.reuestInterstitialAd();
    }

    // Update is called once per frame
    void Update()
    {

        currentLevelText.text = currentLevelIndex.ToString();
        nextLevelText.text = (currentLevelIndex+1).ToString();

        int progress = numOfPassedRIngs * 100 / FindObjectOfType<HelixManager>().numOfRings;
        progressSlider.value = progress;

        scoreText.text = score.ToString();

        if (isGameStarted)
        {
            gamePlayPanel.SetActive(isGameStarted);
            startMenuPanel.SetActive(!isGameStarted);
        }

        if (gameOver)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
            if (Input.GetButtonDown("Fire1"))
            {
                if(score > highscore)
                {
                    highscore = score;
                    PlayerPrefs.SetInt(GamePrefBestScoreKey, highscore);
                    PlayGameServicesScript.instance.ReportScore(highscore, "");
                }
                score = 0;
                SceneManager.LoadScene(SceneName1);
            }
            GameAdManager.instance.shoInterstitialAd();
        }

        if (levelCompleted)
        {
            Time.timeScale = 0;
            levelCompletedPanel.SetActive(true);
            
            if (Input.GetButtonDown("Fire1"))
            {
                
                PlayerPrefs.SetInt(PlayerPrefLevelKey, currentLevelIndex + 1);
                SceneManager.LoadScene("Level");
            }
        }
    }
}
