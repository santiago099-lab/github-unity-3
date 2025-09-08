using JetBrains.Annotations;
using System;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class GameManager : MonoBehaviour
{

    public int fruitsNeededTowin = 30;
    public float timeLimit = 120f;

    public TextMeshProUGUI fruitsText;
    public TextMeshProUGUI timerText;


    private int fruitsCollected = 0;
    private float currentTime;
    private bool gameActive = true;
    private bool gameOver = false;

    private void Start()
    {
        currentTime = timeLimit;
        UpdateUI();
    }

    public void FruitCollected()
    {
        if (!gameActive) return;

        fruitsCollected++;
        Debug.Log($"Frutas: {fruitsCollected}/{fruitsNeededTowin}");

        if (fruitsCollected >= fruitsNeededTowin)
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
            fruitsText.text = $"Frutas: {fruitsCollected}/{fruitsNeededTowin}";
        if (timerText != null)
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