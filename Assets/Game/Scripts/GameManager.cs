using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; set; }
    public GameState Gamestate = GameState.Preparing;
    public GameType gametype;

    public static GameObject Player;

    private void Awake()
    {
        instance = this;

        Player = FindObjectOfType<PlayerManager>().gameObject;
    }

#if UNITY_EDITOR

    public void Update()
    {
        if(Input.GetKeyUp(KeyCode.R))
        {
            CanvasManager.instance.SceneLoad();
        }
        else if (Input.GetKeyUp(KeyCode.N))
        {
            GameConfige.LevelID++;
            CanvasManager.instance.SceneLoad();
        }
        if (Input.GetKeyUp(KeyCode.P))
        {
            GameConfige.LevelID--;
            CanvasManager.instance.SceneLoad();
        }
    }

#endif

    public void GameStart()
    {
        Gamestate = GameState.Playing;
    }

    public void GameOver()
    {
        
        Gamestate = GameState.Over;
        CanvasManager.instance.GameOver();

    }

  

    public void GameComplete()
    {
        GameConfige.LevelID++;
        Gamestate = GameState.Complete;
        CanvasManager.instance.GameComplete();
    }

    public static void Addcoin(int _addamount)
    {
        GameConfige.Coin += _addamount;
    }

    public void ObjectMatching()
    {
        gametype = GameType.ObjectMatching;
        PlayerPrefs.SetString("GameType", (gametype).ToString());
    }
    public void AlphabetMatching()
    {
        gametype = GameType.AlphabetMatching;
        PlayerPrefs.SetString("GameType", (gametype).ToString());
    }
    public void Tracing()
    {
        gametype = GameType.Tracing;
        PlayerPrefs.SetString("GameType", (gametype).ToString());
    }
    public void Train()
    {
        gametype = GameType.Train;
        PlayerPrefs.SetString("GameType", (gametype).ToString());
    }
    public static void MinusCoin(int _amount)
    {
        if (GameConfige.Coin >= _amount)
        {
            GameConfige.Coin -= _amount;
        }
    }

}
