using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance
    {
        get;
        private set;
    }

    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnPaused;

    public enum GameState
    {
        WaitingToStart,
        CountDownToStart,
        GamePlaying,
        GameOver
    }

    private GameState state;
    //private float waitingToStartTime = 1f;
    private float countDownToStartTime = 3f;
    private float playingTime;
    private float playingTimeMax = 60f;
    private bool isPaused;

    private void Awake()
    {
        Instance = this;
        state = GameState.WaitingToStart;
        playingTime = playingTimeMax;
        isPaused = false;
    }

    private void Start()
    {
        GameInput.instance.OnPauseAction += GameInput_OnPauseAction;
        GameInput.instance.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if (state == GameState.WaitingToStart)
        {
            state = GameState.CountDownToStart;
            OnStateChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        TooglePauseGame();
    }

    public void TooglePauseGame()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            Time.timeScale = 0f;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1f;
            OnGameUnPaused?.Invoke(this, EventArgs.Empty);
        }
    }

    private void Update()
    {
        switch (state)
        {
            case GameState.WaitingToStart:
                break;
            case GameState.CountDownToStart:
                countDownToStartTime -= Time.deltaTime;
                if (countDownToStartTime <= 0f)
                {
                    state = GameState.GamePlaying;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case GameState.GamePlaying:
                playingTime -= Time.deltaTime;
                if (playingTime <= 0f)
                {
                    state = GameState.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
        }
        Debug.Log(state);
    }

    public bool isPlaying()
    {
        return state == GameState.GamePlaying;
    }

    public bool isCountingDownToStart()
    {
        return state == GameState.CountDownToStart;
    }

    public float GetCountDownTime()
    {
        return countDownToStartTime;
    }

    public bool isGameOver()
    {
        return state == GameState.GameOver;
    }

    public float GetPlayingTimeClock() 
    {
        return 1 - playingTime / playingTimeMax;
    }
}
