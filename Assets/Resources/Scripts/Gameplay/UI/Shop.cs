using TMPro;
using UnityEngine;
using UnityEngine.Events;
using System.IO;
using System;

public class Shop : MonoBehaviour
{
    public static Shop inst;

    //public Animator animator;

    public int money;
    public string playerName;

    public const string moneyKey = "$";
    private const string moneyFilePath = "money.json";


    void Awake()
    {
        inst = this;
        LoadPlayerInfo();
    }

    void LoadPlayerInfo()
    {
        if(File.Exists(GetPlayerInfoFilePath()))
        {
            string json = File.ReadAllText(GetPlayerInfoFilePath());
            MoneyData moneyData = JsonUtility.FromJson<MoneyData>(json);
            this.money = moneyData.money;
        }
        else
        {
            this.money = 0;
        }
    }

    private string GetPlayerInfoFilePath()
    {
        return Path.Combine(Application.persistentDataPath, moneyFilePath);
    }


    private void OnDisable()
    {
        SavePlayerInfo();
    }

    void Reset()
    {
        File.Delete(GetPlayerInfoFilePath());
    }


    public void AddMoney(int m) {

        
        money += m;
        SavePlayerInfo();
    }

    private void SavePlayerInfo()
    {
        PlayerInfo playerInfo = new PlayerInfo
        {
            playerName = this.playerName,
            funds = this.money,
            lastSaveTimestamp = DateTime.Now.ToString("o")
        };
        string json = JsonUtility.ToJson(playerInfo);
        File.WriteAllText(GetPlayerInfoFilePath(), json);
    }

    private void SaveFunds()
    {
        MoneyData moneyData = new MoneyData {  money = this.money };
        string json = JsonUtility.ToJson(moneyData);
        File.WriteAllText(GetPlayerInfoFilePath(), json);
    }

    [System.Serializable]
    private class MoneyData
    {
        public int money;
    }
}
