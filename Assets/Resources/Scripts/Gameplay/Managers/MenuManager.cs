using SkySentinel.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MenuManager : Singleton<MenuManager>
{
    [Header("Main Menu")]
    public UnityEvent mainMenu;

    [Space]
    [Space]
    [Header("Level Select Screen")]
    public UnityEvent levelSelect;

    [Space]
    [Space]
    [Header("Entire Game")]
    public UnityEvent game;


    private bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        ShowMainMenu();
    }


    public void ShowMainMenu()
    {
        mainMenu.Invoke();

        // Pausing the game when in the main menu
        Time.timeScale = 0f;
    }

    public void StartGame()
    {
        game.Invoke();
        Time.timeScale = 1f;
    }

    public void ShowMissionMenu()
    {
        levelSelect.Invoke();

        Time.timeScale = 0f;
    }

    public void ShowOptionsMenu()
    {

    }

    public void ShowPauseMenu()
    {
        isPaused = !isPaused;
        // Pause menu event
        
        if(isPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void ResumeGame()
    {
        isPaused = false;
        // Disable pause display
        // Enable game UI
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        // Destroy enemies
        // Disable environmen
        // Return to main menu
    }

    public void BackToMainMenu()
    {

    }    
}
