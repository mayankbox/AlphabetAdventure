using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager instance { get; set; }

    public GameObject SplashPanel;
    public GameObject HomePanel;
    public GameObject StartPanel;
    public GameObject PlayingPanel;
    public GameObject GameOverPanel;
    public GameObject GameCompletePanel;
    public GameObject SettingPanel;
    public GameObject LevelPanel;
    public GameObject PausePanel;
    public GameObject ExitPanel;

    [SerializeField]
    Image levelProgressImage;

    [Header("Level Text")]
    [SerializeField]
    Text levelnotxt;
    public List<GameObject> Levels = new List<GameObject>();


    [Header("Setting")]
    public Toggle MusicToggle;
    public Toggle SoundToggle;
    public AudioSource MusicAudiosource;
    public AudioSource SoundAudiosource;
    static bool isFirstTime;


    [Header("ObjectMatchingOBJS")]
    public GameObject playObjectMatching;
    public GameObject ObjectMatchingManager;
    public GameObject ObjectMatchingWin;


    [Header("AlphabetMatchingOBJS")]
    public GameObject playAlphabetMatching;
    public GameObject AlphabetMatchingManager;
    public GameObject AlphabetMatchingwin;


    [Header("TrainOBJS")]
    public GameObject TicketCheck;
    public GameObject playTrain;
    public GameObject TrainManagerr;
    public GameObject Trainwin;

    private void Awake()
    {
        instance = this;

        PlayerPrefs.SetInt("UnlockLevelID_" + 0, 1);
        levelnotxt.text = "Level" + (GameConfige.LevelID+1).ToString();

       // SplashPanel.SetActive(true);
        HomePanel.SetActive(false);
        StartPanel.SetActive(false);
        PlayingPanel.SetActive(false);
        GameOverPanel.SetActive(false);
        GameCompletePanel.SetActive(false);
        ExitPanel.SetActive(false);
        SoundToggle.gameObject.SetActive(false);
        SetMusicToggle();
        SetSoundToggle();

    }
    private void Start()
    {
        if (isFirstTime == false)
        {
             SplashPanel.SetActive(true);
            isFirstTime = true;
            StartCoroutine(SplashClick());
        }
        else
        {

            if (PlayerPrefs.GetInt("IsHomeFalse") == 0)
            {
                SplashPanel.SetActive(false);
                HomePanel.SetActive(true);
                StartPanel.SetActive(false);
                SoundToggle.gameObject.SetActive(true);

            }
            else
            {
                SoundToggle.gameObject.SetActive(true);
                GameStart();
                PlayerPrefs.SetInt("IsHomeFalse", 0);
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)&&HomePanel.activeInHierarchy==true)
        {
            ExitPanel.SetActive(true);
        }
    }

    public void ExitYes()
    {
        Application.Quit();
    }
    public void ExitNo()
    {
        ExitPanel.SetActive(false);
    }
    IEnumerator SplashClick()
    {
        yield return new WaitForSeconds(5.5f);
        SoundToggle.gameObject.SetActive(true);

        HomePanel.SetActive(true);
        SplashPanel.SetActive(false);
    }
    public void ClickOnPlayBtn()
    {
        SoundManager.instance.SoundPlay(SoundName.ButtonCllick);
        HomePanel.SetActive(false);
        StartPanel.SetActive(true);
    }



    public void GameStart()
    {
        SoundManager.instance.SoundPlay(SoundName.ButtonCllick);

        
        GameManagerOwn.instance.GameStart();

        switch(PlayerPrefs.GetString("GameType"))
        {
            case "ObjectMatching":
                AdManager.Instance.ShowInterstitialAd(() =>
                {
                    ObjectMatchingMode();
                    HomePanel.SetActive(false);
                    StartPanel.SetActive(false);
                    PlayingPanel.SetActive(true);
                });

                break;
            case "AlphabetMatching":
                AdManager.Instance.ShowInterstitialAd(() =>
                {
                    AlphabetMatchingMode();
                    HomePanel.SetActive(false);
                    StartPanel.SetActive(false);
                    PlayingPanel.SetActive(true);
                });

                break;
            case "Train":
                //AdManager.Instance.ShowInterstitialAd(() =>
                //{
                    TrainMode();
                    HomePanel.SetActive(false);
                    StartPanel.SetActive(false);
                    PlayingPanel.SetActive(true);
               // });

                break;
            case "Tracing":
                TracingMode();
                break;
        }
    }

    void ObjectMatchingMode()
    {
        GameManagerOwn.instance.gametype = GameType.ObjectMatching;
        playObjectMatching.SetActive(true);
        ObjectMatchingManager.SetActive(true);
        ObjectMatching.instance.GenrateMainTxt();
        ObjectMatching.instance.HandIMG.gameObject.SetActive(false);
        ObjectMatchingWin.SetActive(true);

        if(TicketCheck.activeSelf==true)
        {
            TicketCheck.SetActive(false);
        }

    }
    void AlphabetMatchingMode()
    {
        GameManagerOwn.instance.gametype = GameType.AlphabetMatching;
        playAlphabetMatching.SetActive(true);
        AlphabetMatchingManager.SetActive(true);
        AlphabetMatchingwin.SetActive(true);
        if (TicketCheck.activeSelf == true)
        {
            TicketCheck.SetActive(false);
        }
    }
    void TrainMode()
    {
        
        TicketCheck.SetActive(true);
        Invoke("TicketCollecter", 2f);
    }

   public void TicketCollecter()
    {
        if (GameManagerOwn.instance.ticketCounter == 3)
        {
            TicketCheck.SetActive(false);

            GameManagerOwn.instance.gametype = GameType.Train;
            playTrain.SetActive(true);
            TrainManagerr.SetActive(true);
            Trainwin.SetActive(true);

            StartCoroutine(TrainManager.instance.TrainMove());

        }
    }

    void TracingMode()
    {
        GameManagerOwn.instance.gametype = GameType.Tracing;
        if (TicketCheck.activeSelf == true)
        {
            TicketCheck.SetActive(false);
        }

    }

    public void GameOver()
    {
        PlayingPanel.SetActive(false);
        GameOverPanel.SetActive(true);
    }

    public void GameComplete()
    {
        PlayerPrefs.SetInt("UnlockLevelID_" + GameConfige.LevelID, 1);
        PlayingPanel.SetActive(false);
        GameCompletePanel.SetActive(true);
    }

    public void LevelPanelOpen(bool toggle)
    {
        SoundManager.instance.SoundPlay(SoundName.ButtonCllick);
        if (toggle)
        {
            LevelStatusCheck();
            LevelPanel.SetActive(true);
            StartPanel.SetActive(false);
        }
        else
        {
            LevelPanel.SetActive(false);
            StartPanel.SetActive(true);
        }
    }

    public void OpenLevel(int levelNo)
    {
        SoundManager.instance.SoundPlay(SoundName.ButtonCllick);
        if (PlayerPrefs.GetInt("UnlockLevelID_" + levelNo, 0) == 1)
        {
            GameConfige.LevelID = levelNo;
            SceneLoad();
        }
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;
        SceneLoad();
    }

    public void RestartLoad()
    {
        PlayerPrefs.SetString("GameType", (GameManagerOwn.instance.gametype).ToString());
        Time.timeScale = 1;
        SceneLoad();
        PlayerPrefs.SetInt("IsHomeFalse", 1);
    }
    public void SceneLoad()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void UpdateLevelProgress(float _currentValue, float _totallenght)
    {
        levelProgressImage.fillAmount = _currentValue / _totallenght;
    }

  

    #region Settings

    public void SettingPanelToggle(bool open)
    {
        SoundManager.instance.SoundPlay(SoundName.ButtonCllick);

        if (open)
        {

            SettingPanel.SetActive(true);
            HomePanel.SetActive(false);
        }
        else
        {
            SettingPanel.SetActive(false);
            HomePanel.SetActive(true);
        }

    }

    public void SetMusicToggle()
    {
        print(" ==Music== 000    >>>> " + GameConfige.Music);
        if (GameConfige.Music == 0)
        {
            MusicToggle.isOn = true;
            MusicAudiosource.volume = 0;
        }
        else
        {
            MusicToggle.isOn = false;
            MusicAudiosource.volume = .3f;
        }
    }

    public void SetMusicToogle(Toggle toggle)
    {
        if (toggle.isOn)
        {
            GameConfige.Music =0;
            MusicAudiosource.volume = 0;
            if (SettingPanel.activeSelf)
            {
                SoundManager.instance.SoundPlay(SoundName.ButtonCllick);
            }

        }
        else
        {
            GameConfige.Music = 1;
            MusicAudiosource.volume = 0.3f;
            if (SettingPanel.activeSelf)
            {
                SoundManager.instance.SoundPlay(SoundName.ButtonCllick);
            }
        }
        print(" ==Music== >>>> " + GameConfige.Music);
    }


    public void SetSoundToggle()
    {
        print(" ==Sound== 000    >>>> " + GameConfige.Sound);
        if (GameConfige.Sound == 0)
        {
            SoundToggle.isOn = true;
            SoundAudiosource.volume = 0;
            MusicAudiosource.volume = 0;
        }
        else
        {
            SoundToggle.isOn = false;
            SoundAudiosource.volume = 1;
            MusicAudiosource.volume = 1;

        }
    }

    public void SetSoundToogle(Toggle toggle)
    {
        if (toggle.isOn)
        {
            GameConfige.Sound = 0;
            SoundAudiosource.volume = 0;
            MusicAudiosource.volume = 0;

        }
        else
        {
            GameConfige.Sound = 1;
            SoundAudiosource.volume = 1;
            MusicAudiosource.volume = 1;

            if (SettingPanel.activeSelf)
            {
                SoundManager.instance.SoundPlay(SoundName.ButtonCllick);
            }
        }
        print(" ==Sound== >>>> " + GameConfige.Sound);
    }
    #endregion


    public void LevelStatusCheck()
    {
        for (int i = 0; i < Levels.Count; i++)
        {
            if (PlayerPrefs.GetInt("UnlockLevelID_" + i, 0) == 1)
            {
                Levels[i].GetComponent<LevelBtn>().Unlocked();
            }
            else
            {
                Levels[i].GetComponent<LevelBtn>().locked();
            }
        }
    }


    public void PauseToggele(bool toggle)
    {
        if(toggle)
        {
            Time.timeScale = 0;
            PausePanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            PausePanel.SetActive(false);
        }
    }
}
