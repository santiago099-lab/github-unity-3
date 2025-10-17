using JetBrains.Annotations;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    
    [Header("Level Configuration")]
    public LevelData currentLevelData;


    public TMPro.TextMeshProUGUI fruitsText;
    public TMPro.TextMeshProUGUI timerText;


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

        ReconnectUIReferences();

        UpdateUI();


    }

    private void ReconnectUIReferences()
    {
        GameObject CanvasObj = GameObject.Find("Canvas");
        Debug.Log("Canvas encontrado: " + (CanvasObj != null));

        if (CanvasObj != null)
        {
            Transform Canvas = CanvasObj.transform;

            Transform fruitsPath = Canvas.Find("fruitscounts/contador de frutas/contador de frutas");
            Debug.Log("FruitsText encontrado: " + (fruitsText != null));
            if (fruitsPath != null)
            {
                fruitsText = fruitsPath.GetComponent<TMPro.TextMeshProUGUI>();
                Debug.Log("FruitsText asignado: " + (fruitsText != null));
            }
            Transform timerPath = Canvas.Find("time/tiempo/tiempo");
            Debug.Log("TimerPath encontrado: " + (timerPath != null));
            if (timerPath != null)
            {
                timerText = timerPath.GetComponent<TMPro.TextMeshProUGUI>();
                Debug.Log("TimerText asignado: " + (timerText != null));
            }
        }
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
        StartCoroutine(RestartAfterDelay(1f));
    }

    private System.Collections.IEnumerator RestartAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        RestartGame();
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
        fruitsCollected = 0;

        if (currentLevelData != null)
        {
            currentTime = currentLevelData.timeLimit;
        }
        else
        {
            currentTime = 120f;
        }

        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);


    }

    private System.Collections.IEnumerator ReconnectAfterSceneLoad()
    {
        yield return null; 
        GameObject fruitsTextObj = GameObject.Find("Frutas");
        if (fruitsTextObj != null)
        {
            fruitsText = fruitsTextObj.GetComponent<TMPro.TextMeshProUGUI>();
        }

        GameObject timerTextObj = GameObject.Find("tiempo");
        if (timerTextObj != null)
        {
            timerText = timerTextObj.GetComponent<TMPro.TextMeshProUGUI>();
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

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;

        GameObject Canvas = GameObject.Find("Canvas");
        Debug.Log("Canvas encontrado: " + (Canvas != null));

        if (Canvas != null)
        {
            Transform CanvasObj = Canvas.transform;

            Transform fruitsPath = CanvasObj.Find("fruitscounts/contador de frutas/contador de frutas");
            Debug.Log("FruitsText encontrado: " + (fruitsText != null));
            if (fruitsPath != null)
            {
                fruitsText = fruitsPath.GetComponent<TMPro.TextMeshProUGUI>();
            }

            Transform timerPath = CanvasObj.Find("time/tiempo/tiempo");
            Debug.Log("TimerPath asignado: " + (timerPath != null));
            if (timerPath != null)
            {
                timerText = timerPath.GetComponent<TMPro.TextMeshProUGUI>();
                Debug.Log("TimerText asignado: " + (timerText != null));
            }
        }

        UpdateUI();
    }
}