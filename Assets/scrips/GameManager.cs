using JetBrains.Annotations;
using System;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Level Configuration")]
    public LevelData currentLevelData;


    public TextMeshProUGUI fruitsText;
    public TextMeshProUGUI timerText;


    private int fruitsCollected = 0;
    private float currentTime;
    private bool gameActive = true;
    private bool gameOver = false;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
           Destroy(this.gameObject);
           return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
       if (currentLevelData != null)
        {
            currentTime = currentLevelData.timeLimit;
        }
        else
        {
            currentTime = 120f; 
        }
       UpdateUI();
    }

    public void FruitCollected()
    {
        if (!gameActive) return;

        fruitsCollected++;
        Debug.Log($"Frutas: {fruitsCollected}/{currentLevelData.fruitsNeeded}");

        if (fruitsCollected >= currentLevelData.fruitsNeeded)
        {
            Victory();
        }
    }

    private void Victory()
    {
        gameActive = false;
        Debug.Log("¡Victoria! Has recolectado todas las frutas.");
        Time.timeScale = 0f;
    }

    public void PlayerDied()
    {
        if (gameOver) return;

        gameOver = true;
        gameActive = false;    
        gameActive = false;
        Debug.Log("¡se acabo el tiempo! ");
        Time.timeScale = 0f;
    }
    private void UpdateUI()
    {
        if (fruitsText != null)
        {
            fruitsText.text = $"Frutas: {fruitsCollected}/{currentLevelData.fruitsNeeded}";
        }
        else
        {
            fruitsText.text = "Frutas: 0/30";
        }
        if (currentLevelData != null && timerText != null)
        {
            int minutes = Mathf.FloorToInt(currentTime / 60f);
            int seconds = Mathf.FloorToInt(currentTime % 60f);
            timerText.text = $"Tiempo: {minutes:00}:{seconds:00}";
        }
    }
    public void RestartGame()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    private void UpdateGameTimer()
    {
        if (!gameActive) return;

        currentTime -= Time.deltaTime;
        UpdateUI();

        if (currentTime <= 0)
        {
            TimeUp();
        }
    }

    private void TimeUp()
    {
        gameActive = false;
        Debug.Log("¡se acabo el tiempo! ");
        Time.timeScale = 0f;
    }

    private void Update()
    {
        UpdateGameTimer();
    }
}