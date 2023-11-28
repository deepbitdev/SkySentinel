using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SessionResultsManager : MonoBehaviour
{
    // Singleton instance for easy access
    private static SessionResultsManager _instance;
    public static SessionResultsManager Instance => _instance;

    // Reference to UI elements for displaying results
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI hitsText;
    public TextMeshProUGUI missesText;
    public TextMeshProUGUI waveNumberText;
    public TextMeshProUGUI moneyText;
    public GameObject resultsPanel;

    // Session result variables
    private int score = 0;
    private int hits = 0;
    private int misses = 0;
    private int waveNumber = 0;
    private int money = 0;

    private void Awake()
    {
        // Singleton pattern to ensure only one instance exists
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        resultsPanel.SetActive(false);
    }

    // Function to update session results
    public void UpdateResults(int hits, int misses, int waveNumber)
    {
        this.hits = hits;
        this.misses = misses;
        this.waveNumber = WaveMan.inst.wave;
        this.money = Shop.inst.money;
        score += hits; // You can adjust how the score is calculated based on your game logic

        // Update UI text elements
        scoreText.text = "Score: " + score;
        hitsText.text = "Hits: " + hits;
        missesText.text = "Misses: " + misses;
        waveNumberText.text = "Wave: " + waveNumber;

        // Show the results panel
        resultsPanel.SetActive(true);
    }

    // Function to reset session results for the next wave
    public void ResetResults()
    {
        // Reset variables
        score = 0;
        hits = 0;
        misses = 0;
        waveNumber = 0;

        // Hide the results panel
        resultsPanel.SetActive(false);
    }
}
