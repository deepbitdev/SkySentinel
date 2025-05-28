using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ControllerHUDUI : MonoBehaviour
{
    public static ControllerHUDUI instance;

    [Header("Text Elements")]
    public TextMeshProUGUI softCurrencyText;
    public TextMeshProUGUI hardCurrencyText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI xpText;

    [Header("XP Bar")]
    public Slider xpSlider;

    void Awake()
    {
        if(instance == null) instance = this;
        else Destroy(gameObject);

        UpdateHardCurrencyUI(0);
        UpdateSoftCurrencyUI(0);
        UpdateScoreUI(0);
        UpdateXPUI(0, 100, 1);
    }

    public void UpdateSoftCurrencyUI(int amount)
    {
        softCurrencyText.text = "Coins: " + amount.ToString();
    }
    public void UpdateHardCurrencyUI(int amount)
    {
        hardCurrencyText.text = "Gems: " + amount.ToString();
    }

    public void UpdateScoreUI(int score)
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public void UpdateXPUI(int xp, int xpToNext, int level)
    {
        xpText.text = "XP: " + xp.ToString();
        xpSlider.maxValue = xpToNext;
        xpSlider.value = xp;
    }
}
