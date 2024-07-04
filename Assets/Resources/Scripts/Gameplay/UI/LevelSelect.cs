using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelect : MonoBehaviour
{
    //public GameObject[] environments;

    public void SelectLevelOne()
    {
        WaveMan.inst.SetWaveLimit("LevelOne");
        MenuManager.Instance.ShowTutorialMenu();

    }

    public void SelectLevelTwo()
    {
        WaveMan.inst.SetWaveLimit("LevelTwo");
        MenuManager.Instance.ShowTutorialMenu();
    }

    public void SelectLevelThree()
    {
        WaveMan.inst.SetWaveLimit("LevelThree");
        MenuManager.Instance.ShowTutorialMenu();
    }

    public void SelectLevelFour()
    {
        WaveMan.inst.SetWaveLimit("LevelFour");
        MenuManager.Instance.ShowTutorialMenu();
    }
}
