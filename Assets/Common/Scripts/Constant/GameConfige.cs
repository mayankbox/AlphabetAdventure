using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfige : MonoBehaviour
{
  
    public static int LevelID
    {
        get { return PlayerPrefs.GetInt("LevelID", 0); }
        set {
            if(value < 0)
            {
                value = 0;
            }
            PlayerPrefs.SetInt("LevelID", value);
        }
    }

    public static int Coin
    {
        get { return PlayerPrefs.GetInt("Coin", 0); }
        set { PlayerPrefs.SetInt("Coin", value); }
    }

    public static int CurrentLevel
    {
        get {
            if (LevelManager.instance.Levelmode == LevelMode.Repeat)
            {
                
                return LevelID % LevelManager.TotalLevels;
               
            }
            else
            {
                return Random.Range(LevelManager.instance.RandomAfter -1, LevelManager.TotalLevels);
            }
        }
    }

    public static int Music
    {
        get { return PlayerPrefs.GetInt("Music", 1); }
        set { PlayerPrefs.SetInt("Music", value); }
    }

    public static int Sound
    {
        get { return PlayerPrefs.GetInt("Sound", 1); }
        set { PlayerPrefs.SetInt("Sound", value); }
    }

}
