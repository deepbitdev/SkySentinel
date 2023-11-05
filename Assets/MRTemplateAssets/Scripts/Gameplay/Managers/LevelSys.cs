using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSys : MonoBehaviour
{
    public static LevelSys instance;
    
    public int level;
    public float curXp;
    public float requiredXp;

    private float lerpTimer;
    private float delayTimer;

    public const string xpKey = "xp";
    

    [Header("Text Label")] 
    public TextMeshProUGUI levelTxt;

    
    [Header("Multipliers")]
    [Range(1f, 300f)]
    public float additionMultiplier = 300;
    [Range(2f, 4f)]
    public float powerMultiplier = 2;
    [Range(7f, 14f)]
    public float divisionMultiplier = 7;

    void Awake()
    {
        instance = this;

        //levelTxt.text = level.ToString();

        levelTxt.text = PlayerPrefs.GetInt("level").ToString();
    }
    

    void Update()
    {
        //UpdateXpUI();
        
        
        if(Input.GetKeyDown(KeyCode.Equals))
            GainExperienceFlatRate(20);
        
        if(curXp > requiredXp)
            LevelUp();
    }

    //public void UpdateXpUI()
    //{
    //    float xpFraction = curXp / requiredXp;
    //    float FXP = frontXpBar.fillAmount;
    //    if (FXP < xpFraction)
    //    {
    //        delayTimer += Time.deltaTime;
    //        backXpBar.fillAmount = xpFraction;
    //        if (delayTimer > 3)
    //        {
    //            lerpTimer += Time.deltaTime;
    //            float percentComplete = lerpTimer / 4;
    //            frontXpBar.fillAmount = Mathf.Lerp(FXP, backXpBar.fillAmount, percentComplete);
    //        }
    //    }
    //}

    public void GainExperienceFlatRate(float xpGained)
    {
        curXp += xpGained;
        lerpTimer = 0f;
        delayTimer = 0f;
    }

    // public float GainExperienceScalable(float xpGained, int passedLevel)
    // {
    //     if (passedLevel < level)
    //     {
    //         float multiplier = 1 + (level - passedLevel) * 0.1f;
    //         curXp += xpGained * multiplier;
    //     }
    //     else
    //     {
    //         curXp += xpGained;
    //     }
    //
    //     lerpTimer = 0f;
    //     delayTimer = 0f;
    // }

    public void LevelUp()
    {
        PlayerPrefs.SetInt("xp", level);
        PlayerPrefs.Save();

        level++;
        //frontXpBar.fillAmount = 0f;
        //backXpBar.fillAmount = 0f;
        curXp = Mathf.RoundToInt(curXp - requiredXp);
        requiredXp = CalculateRequiredXP();
        levelTxt.text = level.ToString();
    }

    private void OnDisable()
    {
        PlayerPrefs.SetInt("level", level);
        PlayerPrefs.Save();
    }

    private void Reset()
    {
        PlayerPrefs.DeleteAll();
    }

    private int CalculateRequiredXP()
    {
        int solveForRequiredXp = 0;
        for(int levelCycle = 1; levelCycle <= level; levelCycle++)
        {
            solveForRequiredXp += (int)Mathf.Floor(levelCycle +
                                              additionMultiplier * Mathf.Pow(powerMultiplier,
                                                  levelCycle / divisionMultiplier));
        }

        return solveForRequiredXp / 4;
    }

}
