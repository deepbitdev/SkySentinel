using Oculus.Platform;
using Oculus.Platform.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    public static LeaderboardManager instance;

    public List<LeaderboardEntry> lbe;
    public int amount;
    public GameObject[] entryObjs;
    bool isFloat;

    private void Start()
    {
        instance = this;

        //SetScoreInt();
        //SetScoreFloat();

        GetScoreInt();
        //GetScoreFloat();
    }
    private void Awake()
    {
        try
        {
            Core.AsyncInitialize();
            Entitlements.IsUserEntitledToApplication().OnComplete(EntitlementCallback);
        }
        catch (UnityException e)
        {
            Debug.LogError("Platform failed to initialize due to exception");
            Debug.LogException(e);
            UnityEngine.Application.Quit();
        }
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.A))
        {
            SetScoreInt();
            SetScoreFloat();
        }

        if (Input.GetKey(KeyCode.B))
        {
            GetScoreInt();
            GetScoreFloat();
        }
    }

    public void SetScoreInt()
    {
        SubmitScore("HighScoreInt", 25);
    }

    public void SetScoreFloat()
    {
        SubmitScore("HighScoreFloat", 25.225f);
    }

    public void GetScoreInt()
    {
        GetLeaderboardData("HighScoreInt", false);
    }

    public void GetScoreFloat()
    {
        GetLeaderboardData("HighScoreFloat", true);
    }

    void EntitlementCallback(Message message)
    {
        if(message.IsError)
        {
            Debug.LogError("You are not entitled to use this application");
            UnityEngine.Application.Quit();
        }
        else
        {
            Debug.Log("You are entitled to use this application");
        }
    }

    public void SubmitScore(string leaderboardname, int score)
    {
        if(score <= 0)
        {
            Debug.Log("Invalid value");
            return;
        }
        Leaderboards.WriteEntry(leaderboardname, score);
        Debug.Log("Data saved to leaderboard");
    }

    public void SubmitScore(string leaderboardname, float score)
    {
        if (score <= 0)
        {
            Debug.Log("Invalid value");
            return;
        }
        float x = score * 1000;
        int y = (int)x;
        Leaderboards.WriteEntry(leaderboardname, y);
        Debug.Log("Data saved to leaderboard");
    }

    public void GetLeaderboardData(string leaderboardname, bool b)
    {
        lbe = new List<LeaderboardEntry>();
        isFloat = b;
        Leaderboards.GetEntries(leaderboardname, amount, LeaderboardFilterType.None, LeaderboardStartAt.Top).OnComplete(LeaderboardGetCallback);
    }

    private void LeaderboardGetCallback(Message<LeaderboardEntryList> message)
    {
        if(!message.IsError)
        {
            var entries = message.Data;
            foreach(LeaderboardEntry entry in entries)
            {
                if(entry.User != null)
                {
                    Debug.Log("User ID:" + entry.User.OculusID);
                }
                else
                {
                    Debug.Log("User object is null for an entry");
                }

                lbe.Add(entry);
            }

            // var entries = message.Data;
            // foreach(var entry in entries)
            // {
            //     Users.GetLoggedInUser().OnComplete(
            //         (Message<User>message) =>
            //         {
            //             if(entry.User != null)
            //             {
            //                 Debug.Log("User ID:" + entry.User.OculusID);
            //             }
            //             else
            //             {
            //                 Debug.Log("User object is null for an entry");
            //             }

            //             lbe.Add(entry);
            //         }
            //     );
                
            // }

            Debug.Log("Leaderboards fetcted successfully");
            UpdateUI();
        }
        else
        {
            Debug.Log("Error getting the leaderboards");
        }
    }

    private void UpdateUI()
    {
        for(int i = 0; i < entryObjs.Length; i++) 
        { 
            if(i < lbe.Count)
            {
                entryObjs[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "" + lbe[i].Rank;
                
                if(lbe[i].User != null && !string.IsNullOrEmpty(lbe[i].User.OculusID))
                {
                    entryObjs[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "" + lbe[i].User.OculusID;
                    Debug.Log("You are logged in as..." + lbe[i].User.OculusID);
                }
                else
                {
                    entryObjs[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Unknown User";
                    Debug.Log("User or OculusID is null for leaderboard entry.");
                }

                

                switch(isFloat)
                {
                    case true:
                        float f = (float)lbe[i].Score / 1000;
                        entryObjs[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "" + f.ToString("00,000") + " ";
                        break;
                    case false:
                        entryObjs[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "" + lbe[i].Score;
                        break;
                }
            }
            else
            {
                entryObjs[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "" + (i + 1);
                entryObjs[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "No Data";
                entryObjs[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "";
            }
        }
    }
}
