using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleManager : MonoBehaviour
{
    [HideInInspector] public int length;
    [HideInInspector] public int strength;
    [HideInInspector] public int offLineEarning;
    [HideInInspector] public int lengthCost;
    [HideInInspector] public int strengthCost;
    [HideInInspector] public int offLineEarningCost;
    [HideInInspector] public int Wallet;
    [HideInInspector] public int TotalGain;
    [SerializeField] private GameObject settongPanel;
    private int[] costs = new int[] 
    {
        50,
        99,
        133,
        180,
        220,
        300,
        387,
        450,
        550,
        666,
        777, 
        850,
        900,
        2000
    };

    public static IdleManager instance;

    void Awake()
    {
        // PlayerPrefs.DeleteAll();
        if(instance != null)
        {
            Debug.Log("distroy IdleManager");
            Destroy(gameObject);
        }
        else
            instance = this; 
        
        length = -PlayerPrefs.GetInt("length", 30);
        strength = PlayerPrefs.GetInt("strength",3);
        offLineEarning = PlayerPrefs.GetInt("offLineEarning",3);
        lengthCost = costs[-length / 10 -3];
        strengthCost = costs[strength - 3];
        offLineEarningCost = costs[offLineEarning - 3];
        Wallet = PlayerPrefs.GetInt("wallet",0);
    }
    void Start()
    {
        Debug.Log(length);
        if(PlayerPrefs.HasKey("Date"))
        {
            DateTime lastLogin = DateTime.Parse(PlayerPrefs.GetString("Date"));
            TimeSpan time = DateTime.Now - lastLogin;
            TotalGain = (int)time.TotalMinutes;
            Debug.Log("Offline Earning " + TotalGain);
             ScreensManager.instance.ChangeScreen(Screens.RETURN);
            
        }
    }


    // private void OnApplicationPause(bool pause)
    // {
    //     if(pause)
    //     {
    //         DateTime now = DateTime.Now;
    //     }
    // }  
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetString("Date", DateTime.Now.ToString());
        Debug.Log(DateTime.Now.ToString());
    }

    public void ByeLength()
    {
        length -=10;
        Wallet -= lengthCost;
        lengthCost = costs[-length /10 -3];
        PlayerPrefs.SetInt("length", -length);
        PlayerPrefs.SetInt("wallet", Wallet);
        ScreensManager.instance.ChangeScreen(Screens.MAIN);
        
    }

    public void ByeStreng()
    {
        strength++;
        Wallet -= strengthCost;
        strengthCost = costs[strength - 3];
        PlayerPrefs.SetInt("strength", strength);
        PlayerPrefs.SetInt("wallet", Wallet);
        ScreensManager.instance.ChangeScreen(Screens.MAIN);
        
    }

    public void ByeOfflineEarning()
    {
        offLineEarning++;
        Wallet -= offLineEarningCost;
        offLineEarningCost = costs[offLineEarning - 3];
        PlayerPrefs.SetInt("offLineEarning", offLineEarning);
        PlayerPrefs.SetInt("wallet", Wallet);
        ScreensManager.instance.ChangeScreen(Screens.MAIN);
        
    }
    public void colletedMoney()
    {
        Wallet +=TotalGain;
        PlayerPrefs.SetInt("wallet", Wallet);
        ScreensManager.instance.ChangeScreen(Screens.MAIN);

    }
    public void colletedDoubleMoney()
    {
        Wallet +=TotalGain*2;
        PlayerPrefs.SetInt("wallet", Wallet);
        ScreensManager.instance.ChangeScreen(Screens.MAIN);

    }
    public void exitButton()
    {
        Application.Quit();
        Debug.Log("Application Quit");
    }
    public void SettiongDesable()
    {
        settongPanel.SetActive(false);
    }

    public void settingEnable()
    {
        settongPanel.SetActive(true);
    }

}