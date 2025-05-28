using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Platform;
using Oculus.Platform.Models;

public class UserAgeGroupManager : MonoBehaviour
{
    public bool ageIsOk;
    public bool entitled;
    public bool ageOK;

    void Awake()
    {
        try
        {
            Core.AsyncInitialize();
            Entitlements.IsUserEntitledToApplication().OnComplete(EntitlementCallback);
            UserAgeCategory.Get().OnComplete(GetUserAgeCallBack);
        }
        catch (UnityException e)
        {
            Debug.LogError("Platform failed to initialize exception.");
            Debug.LogException(e);
        }
        if (PlayerPrefs.HasKey("ageIsOk"))
        {
            if(PlayerPrefs.GetInt("ageIsOk") == 1)
            {
                ageIsOk = true;
            }
            else
            {
                ageIsOk = false;
                // userAgeErrorUI.SetActive(true);
            }
        }
        else
        {
            ageIsOk = false;
        }
        if (PlayerPrefs.HasKey("isEntitled"))
        {
            if (PlayerPrefs.GetInt("isEntitiled") == 1)
            {
                entitled = true;
            }
            else
            {
                entitled = false;
                // entitlementErrorUI.SetActive(true);
            }
        }
        else
        {
            entitled = false;
        }
    }
    
    void Update(){
        if(entitled && ageIsOk)
        {
            //run app
        }
    }

    private void GetUserAgeCallBack(Message<UserAccountAgeCategory> msg)
    {
        var uaac = msg.GetUserAccountAgeCategory();
        if (!msg.IsError)
        {            
            string location = Oculus.Platform.Users.GetLoggedInUserLocale();
            int minAge;
            if (location.ToUpper().Contains("EN-US") || location.ToUpper().Contains("KO") || location.ToUpper().Contains("EN-AU") || location.ToUpper().Contains("PT-BR"))
            {
                minAge = 10;//child
            }
            else if (location.ToUpper().Contains("DE"))
            {
                minAge = 12;//child
            }
            else 
            {
                minAge = 16;//teen
            }
            if (uaac.AgeCategory == AccountAgeCategory.Ch)
            {
                UserAgeCategory.Report(AppAgeCategory.Ch);
                PlayerPrefs.SetString("ageCategory", "Ch");
                if (minAge < 13)
                {
                    ageOK = true;
                    PlayerPrefs.SetInt("ageIsOk", 1);
                }
                else
                {
                    ageOK = false;
                    PlayerPrefs.SetInt("ageIsOk", 0);
                }
            }
            else if (uaac.AgeCategory == AccountAgeCategory.Tn)
            {
                UserAgeCategory.Report(AppAgeCategory.Nch);
                PlayerPrefs.SetString("ageCategory", "Nch");
                ageOK = true;
                PlayerPrefs.SetInt("ageIsOk", 1);
            }
            else if (uaac.AgeCategory == AccountAgeCategory.Ad)
            {
                UserAgeCategory.Report(AppAgeCategory.Nch);
                PlayerPrefs.SetString("ageCategory", "Nch");
                ageOK = true;
                PlayerPrefs.SetInt("ageIsOk", 1);
            }
        }
        else
        {
            Debug.Log(msg.GetError());
        }
    }

    private void EntitlementCallback(Message msg)
    {
        if (!msg.IsError)
        {           
            PlayerPrefs.SetInt("isEntitled", 1);
        }
        else
        {
            PlayerPrefs.SetInt("isEntitled", 0);
        }
    }

}
