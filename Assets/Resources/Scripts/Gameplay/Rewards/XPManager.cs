using UnityEngine;

public class XPManager : MonoBehaviour
{
    public static XPManager instance;

    public int currentXP;

    public int level = 1;
    public int xpToNextLevel = 100;

    void Awake()
    {
        if(instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void AddXP(int amount)
    {
        currentXP += amount;
        while(currentXP >= xpToNextLevel)
        {
            currentXP -= xpToNextLevel;
            LevelUp();
        }
        // Updating UI for Updated XP
        ControllerHUDUI.instance.UpdateXPUI(currentXP, xpToNextLevel, level);
        // UIManager.instance.UpdateXPUI(currentXP);
    }

    void LevelUp()
    {
        level++;
        xpToNextLevel = Mathf.RoundToInt(xpToNextLevel * 1.25f); // Increase XP needed for next level
        Debug.Log("Level up to: " + level);
        // Updating UI for Level Up
        // UIManager.instance.UpdateLevelUI(level);
    }
}
