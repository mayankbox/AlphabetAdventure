using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelManager : MonoBehaviour
{
    [System.Serializable]
    public class LevelData
    {
        // add variable like color etc...
        public GameObject LevelPrefab;
       
    }


    public static LevelManager instance { get; set; }
    public static int TotalLevels = 0;
    public int CurrentLevelNo = 0;
    public LevelMode Levelmode = LevelMode.Repeat;
    public int RandomAfter = 0;         //  random level get after perticuler level
    public List<LevelData> Levels = new List<LevelData>();


    private void Awake()
    {
        instance = this;

        // total levels
        TotalLevels = Levels.Count;
        
    }

    private void Start()
    {
        if (CurrentLevel() != null)
        {
            Instantiate(CurrentLevel().LevelPrefab);
        }

    }


    public LevelData CurrentLevel()
    {
        CurrentLevelNo = GameConfige.CurrentLevel;
       
        return Levels[GameConfige.CurrentLevel];
        
    }

}
