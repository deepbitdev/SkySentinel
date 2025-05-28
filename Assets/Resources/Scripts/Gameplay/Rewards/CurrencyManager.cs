using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager instance;

    public int softCurrency;
    public int hardCurrency;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddSoftCurrency(int amount)
    {
        softCurrency += amount;
        // Update UI or perform any other actions needed
        ControllerHUDUI.instance.UpdateSoftCurrencyUI(softCurrency);
    }

    public void AddHardCurrency(int amount)
    {
        hardCurrency += amount;
        // Update UI or perform any other actions needed
        ControllerHUDUI.instance.UpdateHardCurrencyUI(hardCurrency);
    }

    public bool SpendSoftCurrency(int amount)
    {
        if (softCurrency >= amount)
        {
            softCurrency -= amount;
            // Update UI or perform any other actions needed
            ControllerHUDUI.instance.UpdateSoftCurrencyUI(softCurrency);
            return true;
        }
        return false;
    }
    public bool SpendHardCurrency(int amount)
    {
        if (hardCurrency >= amount)
        {
            hardCurrency -= amount;
            // Update UI or perform any other actions needed
            ControllerHUDUI.instance.UpdateHardCurrencyUI(hardCurrency);
            return true;
        }
        return false;
    }

    public int GetSoftCurrency() => softCurrency;
    public int GetHardCurrency() => hardCurrency;

    public void ResetCurrencies()
    {
        softCurrency = 0;
        hardCurrency = 0;
    }
}
