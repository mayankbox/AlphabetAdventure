using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalWinManager : MonoBehaviour
{
    [SerializeField] GameObject FinalWinPanel;
    [SerializeField] GameObject WinPop;
    [SerializeField] GameObject TraccingTicket;
    public static FinalWinManager instance;

    private void Awake()
    {
        instance = this;
    }
   public void FinalWinShow()
    {
        if(PlayerPrefs.GetInt("TraccingTicket")==0)
        {
            TraccingTicket.SetActive(true);
            PlayerPrefs.SetInt("TraccingTicket", 1);
        }

        FinalWinPanel.SetActive(true);
        Invoke("WinPopOn", 3f);
    }
    void WinPopOn()
    {
        WinPop.SetActive(true);
    }
}
