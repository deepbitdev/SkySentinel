using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class GameManager : MonoBehaviour
{
    [Header("Player Main Menu")]
    public UnityEvent playerMenu;

    [Space]
    [Space]
    [Header("Entire Game")]
    public UnityEvent game;


    void Start()
    {
        playerMenu.Invoke();
    }

    public void OpenGame()
    {
        game.Invoke();
    }

}
