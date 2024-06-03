using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    [Header("Player Funds From JSON file")]
    public int money;

    [Header("Default Player Name")]
    public string playerName = "DefaultPlayer";


    private const string playerInfoFilePath = "PlayerInfo.json";


    void Awake()
    {
        instance = this;
        LoadPlayerInfo();
    }

    void LoadPlayerInfo()
    {
        if (File.Exists(GetPlayerInfoFilePath()))
        {
            string json = File.ReadAllText(GetPlayerInfoFilePath());
            PlayerInfo playerInfo = JsonUtility.FromJson<PlayerInfo>(json);
            this.playerName = playerInfo.playerName;
            this.money = playerInfo.funds;
        }
        else
        {
            // Replacing this with meta username!!!
            this.playerName = "DefaultPlayer";
            this.money = 0;
        }
    }

    private string GetPlayerInfoFilePath()
    {
        return Path.Combine(Application.persistentDataPath, playerInfoFilePath);
    }


    private void OnDisable()
    {
        SavePlayerInfo();
    }

    void Reset()
    {
        File.Delete(GetPlayerInfoFilePath());
    }


    public void AddMoney(int m)
    {
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
}
