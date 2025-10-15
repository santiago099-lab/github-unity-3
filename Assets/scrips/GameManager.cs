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


    public UnityEngine.UI.Text fruitsText;
    public UnityEngine.UI.Text timerText;


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

        gameActive = true;
        gameOver = false;
        fruitsCollected = 0;

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
        Debug.Log("¡se acabo el tiempo! ");
        Time.timeScale = 0f;
    }
    private void UpdateUI()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            return;
        }

        if (fruitsText != null)
        {
            if (currentLevelData != null)
            {
             fruitsText.text = $"Frutas: {fruitsCollected}/{currentLevelData.fruitsNeeded}";
            }
            else
            {
                fruitsText.text = "Frutas: 0/30";
            }
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
        gameOver = false;
        gameActive = true;

        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);

        StartCoroutine(ReconnectAfterSceneLoad());
    }

    private System.Collections.IEnumerator ReconnectAfterSceneLoad()
    {
        yield return null; 
        GameObject fruitsTextObj = GameObject.Find("Frutas");
        if (fruitsTextObj != null)
        {
            fruitsText = fruitsTextObj.GetComponent<UnityEngine.UI.Text>();
        }

        GameObject timerTextObj = GameObject.Find("tiempo");
        if (timerTextObj != null)
        {
            timerText = timerTextObj.GetComponent<UnityEngine.UI.Text>();
        }

        fruitsCollected = 0;
        gameActive = true;
        gameOver = false;

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
        gameOver = true;
        Debug.Log("¡se acabo el tiempo! ");
        Time.timeScale = 0f;
    }

    private void Update()
    {
        UpdateGameTimer();
    }
}