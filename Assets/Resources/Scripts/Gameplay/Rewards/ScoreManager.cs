using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public int currentScore = 0;
    public int enemiesKilled = 0;

    private void Awake()
    {
        if(instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void AddScore(int points)
    {
        currentScore += points;
        // Updating UI for Updated Score
        // UIManager.instance.UpdateScoreUI(currentScore);
    }

    public void AddEnemiesKilled(int rewardAmount)
    {
        enemiesKilled++;
        AddScore(rewardAmount);
        CurrencyManager.instance.AddSoftCurrency(rewardAmount / 2);
        XPManager.instance.AddXP(rewardAmount / 4);
        // Updating UI for Updated Enemies Killed
        // UIManager.instance.UpdateEnemiesKilledUI(enemiesKilled);
    }

    public int GetScore() => currentScore;
    public int GetEnemiesKilled() => enemiesKilled;

    // public void ResetScore()
    // {
    //     score = 0;
    // }
}
