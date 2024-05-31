using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WaveMan : MonoBehaviour
{
    public static WaveMan inst;


    public List<Enemy> enemiesPrefabs;
    public float moneyTotEnemies = 100, coefCost = 1.3f;
    float crtMoneyEnemies;
    public float dtEnemy = 0.3f;
    float tNextEnemy;

    public TMP_Text waveInfo;
    //public TMP_Text waveNumber;
    public UnityEvent startWaveEvent, endWaveEvent, successEvent;
    public static bool inWave = false;
    [HideInInspector] public int wave = 1;


    private Dictionary<string, int> waveLimits = new Dictionary<string, int>()
    {
        { "easy", 25},
        {"medium", 35},
        {"hard", 50}
    };

    private int currentWaveLimit = 30;

    void Awake()
    {
        inst = this;

        SetWaveLimit("easy");
    }


    void Update()
    {
        if (inWave) {
            if (crtMoneyEnemies != 0) {
                if (Time.time > tNextEnemy) {

                    List<Enemy> enemiesAvailable = new List<Enemy>();
                    foreach (Enemy e in enemiesPrefabs)
                        if ((e == enemiesPrefabs[0] && e.money <= crtMoneyEnemies) ||
                            (e != enemiesPrefabs[0] && e.money <= crtMoneyEnemies / 2))
                            enemiesAvailable.Add(e);

                    if (enemiesAvailable.Count == 0)
                        crtMoneyEnemies = 0;

                    else {
                        tNextEnemy += dtEnemy;
                        Enemy enemyPrefab = Tool.Rand(enemiesAvailable);
                        crtMoneyEnemies -= enemyPrefab.money;
                        SpawnEnemy(enemyPrefab);
                    }
                }
            }

            else if (Enemy.enemies.Count == 0 && crtMoneyEnemies == 0 && !Base.inst.died)
                EndWave();
        }
        
        if (wave >= currentWaveLimit)
        {
            Debug.Log(" Wave Event Had Ended! ");
            MissionComplete();
        }

        //waveInfo.text = "Wave " + wave.ToString();
        waveInfo.text = "Wave " + wave.ToString() + " / " + currentWaveLimit.ToString();
    }

    public void SetWaveLimit(string label)
    {
        if(waveLimits.ContainsKey(label))
        {
            currentWaveLimit = waveLimits[label];
            Debug.Log($"Wave limit set to {currentWaveLimit} for label '{label}'.");
        }
        else
        {
            Debug.LogError($"Wave limit for label '{label}' is not found.");
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
        crtMoneyEnemies = moneyTotEnemies;
        tNextEnemy = Time.time;

        inWave = true;
        startWaveEvent.Invoke();
    }

    public void EndWave()
    {
        wave++;
        moneyTotEnemies *= coefCost;
        waveInfo.text = "Wave " + wave.ToString() + " / " + currentWaveLimit.ToString();
        //waveInfo.text = "Wave " + wave.ToString();
        //waveNumber.text = wave.ToString();
        inWave = false;
        // LevelSys.instance.GainExperienceFlatRate(20);
        endWaveEvent.Invoke();
    }
}
