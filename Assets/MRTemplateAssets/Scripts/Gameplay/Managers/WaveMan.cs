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

    float crtMoneyEnemies;
    float tNextEnemy;

    public TMP_Text waveInfo;
    //public TMP_Text waveNumber;
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
        waveInfo.text = " Wave " + wave;

        if (inWave) {
            if (crtMoneyEnemies != 0) {
                if (Time.time > tNextEnemy) {

                    List<Enemy> enemiesAvailable = new List<Enemy>();
                    foreach (Enemy e in waveConfig.enemiesPrefabs)
                    {
                        if ((e == waveConfig.enemiesPrefabs[0] && e.money <= crtMoneyEnemies) ||
                            (e != waveConfig.enemiesPrefabs[0] && e.money <= crtMoneyEnemies / 2))
                        {
                            enemiesAvailable.Add(e);
                        }
                    }
                    if (enemiesAvailable.Count == 0)
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
            else if (waveConfig.enemiesPrefabs.Count == 0 && crtMoneyEnemies == 0 && !Base.inst.died)
            {
                EndWave();
            }
        }

        if(wave == waveConfig.maxWaves && !bossBattleTriggered && waveConfig.hasBossBattle)
        {
            StartBossBattle();
        }
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
        
        if(wave <= waveConfig.maxWaves)
        {
            waveConfig.moneyTotEnemies *= waveConfig.coefCost;
            waveInfo.text = " Wave " + wave;
            inWave = false;
            endWaveEvent.Invoke();
        }
        else if(wave == waveConfig.maxWaves + 1 && waveConfig.hasBossBattle)
        {
            StartBossBattle();
        }
        else
        {
            MissionComplete();
        }
    }

    private void StartBossBattle()
    {
        bossBattleTriggered = true;
        SpawnEnemy(waveConfig.bossPrefab);
        waveInfo.text = "Boss Battle!";
        inWave = true;
        startWaveEvent.Invoke();
    }

    public void MissionComplete()
    {
        inWave = false;
        successEvent.Invoke();
    }
}
