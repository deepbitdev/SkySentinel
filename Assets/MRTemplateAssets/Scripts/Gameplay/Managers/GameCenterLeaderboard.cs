using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;

public class GameCenterLeaderboard : MonoBehaviour
{
    void Start()
    {
        Social.localUser.Authenticate(ProcessAuthentication);
    }

    void ProcessAuthentication(bool success)
    {
        if(success) {
            Debug.Log("Authenticated, Checking Achievements");
        } else {
            Debug.Log("Failed to authenticate");
        }
    }

    // void ReportScore(long score, string leaderboardID) {
    //     Debug.Log("Reporting score" + score + " on leaderboard " + leaderboardID);
    //     Social.ReportScore(score, leaderboardID, success => {
    //         Debug.Log(success ? "Reported score successfully" : "Failed to report score");
    //     });
    // }

    void ReportScore(int wave, string leaderboardID)
    {
        Debug.Log("Reporting score" + wave + " on leaderboard " + leaderboardID);
        Social.ReportScore(wave, leaderboardID, success => {
            Debug.Log(success ? "Reported score successfully" : "Failed to report score");
            WaveMan.inst.wave = wave;
        });
    }

    public void PushWaveNumber()
    {
        ReportScore(WaveMan.inst.wave, "1");
    }

    void OnDisable()
    {
        ReportScore(WaveMan.inst.wave, "1");
    }

    

    void ProcessLoadedAchievements(IAchievement[] achievements) {
        if(achievements.Length == 0)
            Debug.Log("Error: no achievements found");
        else
            Debug.Log("Got " + achievements.Length + " achievements");
        
        Social.ReportProgress("Achievement01", 100.0, result => {
            if(result)
                Debug.Log("Successfully reported achievement progress");
            else 
                Debug.Log("Failed to report achievement");
        });
    }
}
