using SkySentinel.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [Header("Mission Success")]
    public UnityEvent successEvent;

    [Header("Mission Failure")]
    public UnityEvent failureEvent;

    public int playerScore = 0;
    public int currentWave = 0;
    public int enemiesDestroyed = 0;

    [Header("Game Status")]
    public TextMeshPro statusTxt;
    public TextMeshPro gameStatusEvntTxt;

    public TextMeshPro scoreTxt;
    public TextMeshPro waveTxt;
    public TextMeshPro enemiesDestroyedTxt;

    void Start()
    {
        UpdateUI();

        statusTxt.text = "";
        gameStatusEvntTxt.text = "";
    }

    private void UpdateUI()
    {
        scoreTxt.text = "Score: " + playerScore.ToString();
        waveTxt.text = "Wave: " + WaveMan.inst.wave.ToString() + " / " + WaveMan.inst.currentWaveLimit.ToString();
        enemiesDestroyedTxt.text = "Enemies Destroyed: " + enemiesDestroyed.ToString();

    }

    public void AddScore(int amount)
    {
        playerScore += amount;
        UpdateUI();
    }

    public void AddEnemiesDestroyed(int amount)
    {
        enemiesDestroyed += amount;
        UpdateUI();
    }

    public void CheckGameOver()
    {

    }


    public void AreaCleared()
    {
        WaveMan.inWave = false;
        successEvent.Invoke();
        Tower.towers.Clear();
        statusTxt.text = "Area Cleared";
        gameStatusEvntTxt.text = "You survived all waves";
        waveTxt.text = "Wave: " + WaveMan.inst.wave.ToString() + " / " + WaveMan.inst.currentWaveLimit.ToString();
    }

    public void GameOver()
    {
        WaveMan.inWave = false;
        failureEvent.Invoke();
        Tower.towers.Clear();
        statusTxt.text = "Defeat";
        waveTxt.text = "Wave: " + WaveMan.inst.wave.ToString() + " / " + WaveMan.inst.currentWaveLimit.ToString();
        gameStatusEvntTxt.text = " Defeated at " + WaveMan.inst.wave.ToString();
    }

    public void RestartGame()
    {
        playerScore = 0;
        enemiesDestroyed = 0;
        UpdateUI();

    }

    public void Reload()
    {
        //setTime(1);
        Enemy.enemies.Clear();
        Tower.towers.Clear();
        WaveMan.inWave = false;
        SessionResultsManager.Instance.ResetResults();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
