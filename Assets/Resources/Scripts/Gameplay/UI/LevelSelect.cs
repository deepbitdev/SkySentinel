using SkySentinel.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelect : Singleton<LevelSelect>
{
    public GameObject[] visualLevels;

    public int currentIndex = 0;


    public void SelectLevelOne()
    {
        WaveMan.inst.SetWaveLimit("LevelOne");
        MenuManager.Instance.ShowTutorialMenu();
    }

    public void SelectLevelTwo()
    {
        WaveMan.inst.SetWaveLimit("LevelTwo");
        MenuManager.Instance.ShowTutorialMenu();
        SwitchVisual();
    }

    public void SelectLevelThree()
    {
        WaveMan.inst.SetWaveLimit("LevelThree");
        MenuManager.Instance.ShowTutorialMenu();
        SwitchVisual();
    }

    public void SelectLevelFour()
    {
        WaveMan.inst.SetWaveLimit("LevelFour");
        MenuManager.Instance.ShowTutorialMenu();
        SwitchVisual();
    }


    void SwitchVisual()
    {
        if(visualLevels.Length > 0)
        {
            visualLevels[currentIndex].SetActive(false);

            currentIndex = (currentIndex + 1) % visualLevels.Length;

            visualLevels[currentIndex].SetActive(true);
        }
    }
}
