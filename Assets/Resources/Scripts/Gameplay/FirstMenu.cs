using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;


public class FirstMenu : MonoBehaviour
{
    public TextMeshProUGUI moneyTxt;
    public TextMeshProUGUI levelTxt;
    public TextMeshProUGUI usernameTxt;
    public TMP_InputField usernameInput;

    [Header(" User profile ")] 
    public GameObject profileCreationPrefab;


    public string playerName;
    public string saveName;

    void Start()
    {
        usernameTxt.text = PlayerPrefs.GetString("user_name");
        moneyTxt.text = PlayerPrefs.GetInt("money").ToString();
        levelTxt.text = " Level : " + PlayerPrefs.GetInt("level").ToString();
    }

    void Update()
    {
        playerName = PlayerPrefs.GetString("user_name", "username");

        if (PlayerPrefs.HasKey("user_name"))
        {
            profileCreationPrefab.SetActive(false);
        }
        else
        {
            profileCreationPrefab.SetActive(true);
        }
        usernameTxt.text = playerName;
    }

    



    public void SaveProfile()
    {
        PlayerPrefs.SetString("user_name", usernameInput.text);
        usernameTxt.text = PlayerPrefs.GetString("user_name");
        profileCreationPrefab.SetActive(false);
        PlayerPrefs.Save();
    }

    public void Reset()
    {
        PlayerPrefs.DeleteAll();
    }



    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .30f);

            // slider.value = progress;
            
            Debug.Log(progress);

            yield return null;
        }
    }
}
