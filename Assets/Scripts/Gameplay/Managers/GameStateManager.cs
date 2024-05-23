using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum GameState
{
    Starting,
    InWave,
    BossBattle, 
    GameOver,
    MissionComplete
}

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;

    public GameState currentState;
    public UnityEvent onGameStart;
    public UnityEvent onWaveStart;
    public UnityEvent onWaveEnd;
    public UnityEvent onBossBattleStart;
    public UnityEvent onMissionComplete;
    public UnityEvent onGameOver;

    public bool bossBattleTriggered = false;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        ChangeState(GameState.Starting);
    }

    private void ChangeState(GameState newState)
    {
        currentState = newState;
        HandleStateChange();
    }

    private void HandleStateChange()
    {
        switch(currentState)
        {
            case GameState.Starting:
                onGameStart.Invoke(); 
                break;
            case GameState.InWave:
                onWaveStart.Invoke();
                break;
            case GameState.BossBattle:
                onBossBattleStart.Invoke();
                break;
            case GameState.GameOver:
                onGameOver.Invoke();
                break;
            case GameState.MissionComplete:
                onMissionComplete.Invoke();
                break;
        }
    }

    public void StartWave()
    {
        ChangeState(GameState.InWave);
        WaveMan.inst.StartWave();
    }

    public void EndWave()
    {
        onWaveEnd.Invoke();
        if (WaveMan.inst.wave == WaveMan.inst.waveConfig.maxWaves && WaveMan.inst.waveConfig.hasBossBattle && !bossBattleTriggered)
        {
            StartBossBattle();
        }
        else if (WaveMan.inst.wave <= WaveMan.inst.waveConfig.maxWaves)
        {
            WaveMan.inst.EndWave();
        }
        else
        {
            ChangeState(GameState.MissionComplete);
        }
    }

    public void StartBossBattle()
    {
        bossBattleTriggered = true;
        ChangeState(GameState.BossBattle);
        WaveMan.inst.StartBossBattle();
    }

    public void MissionComplete()
    {
        ChangeState(GameState.MissionComplete);
        WaveMan.inst.MissionComplete();
    }

    public void GameOver()
    {
        ChangeState(GameState.GameOver);
    }
}
