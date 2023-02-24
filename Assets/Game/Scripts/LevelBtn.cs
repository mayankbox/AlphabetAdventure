using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBtn : MonoBehaviour
{
    public GameObject LevelText;
    public GameObject Lockbtn;

    public void Unlocked()
    {
        LevelText.SetActive(true);
        Lockbtn.SetActive(false);
    }

    public void locked()
    {
        LevelText.SetActive(false);
        Lockbtn.SetActive(true);
    }

    public void ClickLevlOpen(int levelNo)
    {
        CanvasManager.instance.OpenLevel(levelNo);
    }
}
