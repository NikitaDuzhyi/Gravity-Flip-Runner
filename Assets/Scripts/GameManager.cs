using System.Runtime.ExceptionServices;
using TMPro;
using UnityEngine;
using UnityEngine.LightTransport;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform world;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject gamePauseScreen;
    [SerializeField] private GameObject startMenuScreen;

    [SerializeField] private Button restartButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button startButton;

    [SerializeField] private TextMeshProUGUI scoreText;

    [Header("Backgrounds")]
    [SerializeField] private GameObject dayBackground;
    [SerializeField] private GameObject nightBackground;

    public static GameManager Instance;

    private bool isNight;
    private bool paused;
    private bool gameOver;

    private int score;
    public int Score => score;
    public float GameSpeed { get; private set; } = 5f;
    private static bool gameStartedBefore = false;
    private void Awake()
    {
        Instance = this;
        startButton.onClick.AddListener(Begin);
        restartButton.onClick.AddListener(Restart);
        pauseButton.onClick.AddListener(Pause);
    }

    private void Start()
    {
        score = 0;
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "BEST: " +highScore;
        scoreText.text = score.ToString();

        dayBackground.SetActive(true);
        nightBackground.SetActive(false);
        Time.timeScale = 0f;

        isNight = false;
        if (Screen.height / Screen.width > 1.9f)
        {
            world.localScale = new Vector3(0.7f, 1f, 1f);
        }
        if (!gameStartedBefore)
        {
            startMenuScreen.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            startMenuScreen.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    private void Update()
    {
        if (gameOver)
            return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void AddScore(int amount)
    {
        if (isNight)
        {
            amount *= 2;
        }

        score += amount;
        scoreText.text = score.ToString();
        GameSpeed = 5f + score * 0.005f;
        CheckNightMode();
    }

    private void CheckNightMode()
    {
        bool shouldBeNight =
            (score / 100) % 2 == 1;

        if (shouldBeNight == isNight)
            return;

        isNight = shouldBeNight;

        dayBackground.SetActive(!isNight);
        nightBackground.SetActive(isNight);
    }

    public void GameOver()
    {
        gameOver = true;

        int highScore = PlayerPrefs.GetInt("HighScore",0);

        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore",score);
            PlayerPrefs.Save();
        }
        gameOverScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    private void Pause()
    {
        paused = !paused;
        gamePauseScreen.SetActive(paused);
        Time.timeScale = paused ? 0f : 1f;
    }
    private void Begin()
    {
        gameStartedBefore = true;
        startMenuScreen.SetActive(false);
        Time.timeScale = 1f;
    }
    private void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}