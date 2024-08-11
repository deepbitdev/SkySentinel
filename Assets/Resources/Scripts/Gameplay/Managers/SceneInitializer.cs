using SkySentinel.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInitializer : Singleton<SceneInitializer>
{
    // Start is called before the first frame update
    void Start()
    {
        ApplyUserData();

        if(Instance == null)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void ApplyUserData()
    {
        int level = LevelSelect.Instance.currentIndex;

        // Apply the data to the game

        Debug.Log("Level Selected" + LevelSelect.Instance.currentIndex);
    }
}
