using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WaveMan : MonoBehaviour
{
    public static WaveMan inst;

    public WaveConfig waveConfig;

    public float moneyTotEnemies = 100, coefCost = 1.3f;
    float crtMoneyEnemies;
    public float dtEnemy = 0.3f;
    float tNextEnemy;

    public TMP_Text waveInfo;
    public UnityEvent startWaveEvent, endWaveEvent, successEvent;
    public static bool inWave = false;
    [HideInInspector] public int wave = 1;

    private bool bossBattleTriggered = false;

    void Awake()
    {
        inst = this;
    }


    void Update()
    {
        if (inWave)
        {
            if (crtMoneyEnemies != 0)
            {
                if (Time.time > tNextEnemy)
                {

                    List<Enemy> enemiesAvailable = new List<Enemy>();
                    foreach(Enemy e in waveConfig.enemiesPrefabs)
                    {
                        if((e == waveConfig.enemiesPrefabs[0] && e.money <= crtMoneyEnemies) || 
                            (e != waveConfig.enemiesPrefabs[0] && e.money <= crtMoneyEnemies / 2))
                        {
                            enemiesAvailable.Add(e);
                        }
                    }

                    if(enemiesAvailable.Count == 0)
                    {
                        crtMoneyEnemies = 0;
                    }
                    else
                    {
                        tNextEnemy += waveConfig.dtEnemy;
                        Enemy enemyPrefab = Tool.Rand(enemiesAvailable);
                        crtMoneyEnemies -= enemyPrefab.money;
                        SpawnEnemy(enemyPrefab);
                    }
                }
            }
            else if (Enemy.enemies.Count == 0 && crtMoneyEnemies == 0 && !Base.inst.died)
            {
                EndWave();
            }
        }
        
        if (wave == waveConfig.maxWaves && waveConfig.hasBossBattle && GameStateManager.Instance.currentState != GameState.BossBattle)
        {
            Debug.Log(" Wave Event Had Ended! ");
            //MissionComplete();
            StartBossBattle();
        }
    }

    public void MissionComplete()
    {
        successEvent.Invoke();
        inWave = false;
    }

    void SpawnEnemy(Enemy enemyPrefab)
    {
        Enemy enemy = Instantiate(enemyPrefab, Base.inst.gravity.position, Quaternion.identity);
        float oy = Tool.Rand(360);
        float ox = Random.Range(-5, 5);
        enemy.transform.Rotate(ox,oy,0);
        enemy.transform.Translate(Vector3.back * Base.inst.view);
    }

    public void StartWave()
    {

        if(wave <= waveConfig.maxWaves)
        {
            crtMoneyEnemies = waveConfig.moneyTotEnemies;
            tNextEnemy = Time.time;

            inWave = true;
            startWaveEvent.Invoke();
        }
    }

    public void EndWave()
    {
        wave++;
        if (wave <= waveConfig.maxWaves)
        {
            moneyTotEnemies *= waveConfig.coefCost;
            waveInfo.text = "Wave " + wave;
            inWave = false;
            endWaveEvent.Invoke();
        }
        else if(wave == waveConfig.maxWaves + 1 && waveConfig.hasBossBattle)
        {
            GameStateManager.Instance.StartBossBattle();
        }
        else
        {
            GameStateManager.Instance.MissionComplete();
        }
    }

    public void StartBossBattle()
    {
        GameStateManager.Instance.StartBossBattle();
        SpawnEnemy(waveConfig.bossPrefab);
        waveInfo.text = "Boss Battle!";
        inWave = true;
        startWaveEvent.Invoke();
    }
}
