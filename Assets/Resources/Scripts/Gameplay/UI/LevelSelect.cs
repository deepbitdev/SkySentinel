using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelect : MonoBehaviour
{
    //public GameObject[] environments;

    public void SelectLevelOne()
    {
        WaveMan.inst.SetWaveLimit("LevelOne");
        MenuManager.Instance.StartGame();

    }

    public void SelectLevelTwo()
    {
        WaveMan.inst.SetWaveLimit("LevelTwo");
        MenuManager.Instance.StartGame();
    }

    public void SelectLevelThree()
    {
        WaveMan.inst.SetWaveLimit("LevelThree");
        MenuManager.Instance.StartGame();
    }

    public void SelectLevelFour()
    {
        WaveMan.inst.SetWaveLimit("LevelFour");
        MenuManager.Instance.StartGame();
    }
}
