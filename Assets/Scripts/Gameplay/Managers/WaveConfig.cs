using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "ScriptableObjects/WaveConfig", order =1)]
public class WaveConfig : ScriptableObject
{
    public List<Enemy> enemiesPrefabs;
    public float moneyTotEnemies = 100;
    public float coefCost = 1.3f;
    public float dtEnemy = 0.3f;
    public int maxWaves = 10;

    public Enemy bossPrefab;
    public bool hasBossBattle = true;
}
