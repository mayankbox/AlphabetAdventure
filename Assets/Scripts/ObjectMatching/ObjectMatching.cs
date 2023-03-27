using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ObjectMatching : MonoBehaviour
{
    [SerializeField] GameObject MainCamera;
    [SerializeField] Image MainAlphaIMG;
    [SerializeField] Sprite[] AlphaArrayIMG;
    [SerializeField] Sprite[] PicArrayIMG;
    public List<string> WrongCharList = new List<string>();
    bool isWrongAd;
    [SerializeField] Image WrongIMG;
    [SerializeField] Image CorrectIMG;
    [SerializeField] ParticleSystem CorrectPartical;
    public Image HandIMG;
    public int AlphaCounter;
    string aa;
    int RandmNumAlpha;
    int RandmNumThree;
    [SerializeField] float HandMoveSpeed;
    bool IsHandAnim;
    public GameObject[] BTNs;

    float CorrectClickAfterTimmer;
    bool correctClickAfter;


    [SerializeField] float Waittingtimmer;
    [SerializeField] GameObject NextBtnObj;
    [SerializeField] GameObject BackBtnObj;
    bool IsWaitingTimmerStart;

    [Header("Win")]
    [SerializeField] GameObject WinPopUp;
    [SerializeField] GameObject WinGiftImg;
    [SerializeField] GameObject Ballon1Img;
    [SerializeField] GameObject Ballon2Img;
    bool IsCounterMinus;
    int CorrectCounter = 26;
    [SerializeField] Text ScoreTxt;
    [SerializeField] Text GreetingTxt;
    public GameObject[] Stars;
    public int GreetingSoundType;
    int starOnCount;
    [SerializeField] GameObject ObjTicket;

    public static ObjectMatching instance;

    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        if (GameManagerOwn.instance.Gamestate == GameState.Playing)
        {
            if (IsWaitingTimmerStart == true)
            {
                Waittingtimmer -= 1 * Time.deltaTime;
            }
            if (Waittingtimmer <= 0 && IsWaitingTimmerStart)
            {
                IsWaitingTimmerStart = false;
                if (!IsHandAnim)
                {
                    IsHandAnim = true;
                    StartCoroutine(StartHandAnim());
                }
            }

            if (correctClickAfter)
            {
                CorrectClickAfterTimmer += 1 * Time.deltaTime;

                if (CorrectClickAfterTimmer >= 3)
                {
                    NextBTN();
                    correctClickAfter = false;
                    CorrectClickAfterTimmer = 0;
                }
            }
        }
    }
    public void GenrateMainTxt()
    {
        isWrongAd = false;
        MainAlphaIMG.gameObject.SetActive(true);
        foreach (GameObject Btns in BTNs)
        {
            Btns.SetActive(true);
            Btns.GetComponent<Button>().enabled = true;
        }

        IsWaitingTimmerStart = true;
        NextBtnObj.SetActive(false);
        Waittingtimmer = 6f;

        TruFalseIconOnOff();

        MainAlphaIMG.sprite = AlphaArrayIMG[AlphaCounter];
        MainAlphaIMG.name = AlphaArrayIMG[AlphaCounter].name;

        RandmNumThree = Random.Range(0, 3);


        if (AlphaCounter == 0)
        {
            BackBtnObj.SetActive(false);
            IsHandAnim = true;
            StartCoroutine(StartHandAnim());
        }
        else
        {
            BackBtnObj.SetActive(true);
            BackBtnObj.GetComponent<Button>().interactable = true;

            IsHandAnim = false;
            HandIMG.transform.SetParent(null);

            HandIMG.gameObject.SetActive(false);
            StopCoroutine(StartHandAnim());
        }


        for (int i = 0; i < 3; i++)
        {
            if (i == RandmNumThree)
            {
                BTNs[i].transform.GetChild(0).GetComponent<Image>().sprite = PicArrayIMG[AlphaCounter];
                BTNs[i].name = AlphaArrayIMG[AlphaCounter].name;
            }
            else
            {
                RandmNumAlpha = Random.Range(0, AlphaArrayIMG.Length);
                for (int j = 0; j < AlphaArrayIMG.Length; j++)
                {
                    if (RandmNumAlpha != AlphaCounter)
                    {
                        BTNs[i].transform.GetChild(0).GetComponent<Image>().sprite = PicArrayIMG[RandmNumAlpha];
                        BTNs[i].name = BTNs[i].name = AlphaArrayIMG[RandmNumAlpha].name;
                    }
                    else
                    {
                        RandmNumAlpha = Random.Range(0, AlphaArrayIMG.Length);
                    }
                }
            }
        }
    }

    IEnumerator StartHandAnim()
    {


        yield return new WaitForSeconds(.2f);

        if (IsHandAnim)
        {

            if (GameConfige.Music != 0)
            {
                CanvasManager.instance.MusicAudiosource.volume = 0.1f;
            }
            SoundManager.instance.SoundPlay(SoundName.forAlpha);

            HandIMG.gameObject.SetActive(true);
            HandIMG.transform.SetParent(BTNs[RandmNumThree].transform);
            HandIMG.transform.localPosition = Vector3.zero;

            Invoke("AgainHandAnim", 2f);
        }
        else
        {
            if (GameConfige.Music != 0)
            {
                CanvasManager.instance.MusicAudiosource.volume = 0.3f;
            }
        }

    }

    void AgainHandAnim()
    {
        HandIMG.transform.SetParent(BTNs[RandmNumThree].transform);
        HandIMG.transform.localPosition = Vector3.zero;
        if (IsHandAnim)
            StartCoroutine(StartHandAnim());
    }
    public void BtnCheck()
    {
        Waittingtimmer = 6f;
        GameObject aaa = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        aa = aaa.name;
        if (aa == MainAlphaIMG.name)
        {
            if (AlphaCounter % 5 == 0 && AlphaCounter != 0)
            {
                AdManager.Instance.ShowInterstitialAd(() =>
                {

                    foreach (string ss in WrongCharList)
                    {
                        if (ss == AlphaArrayIMG[AlphaCounter].name)
                        {
                            if (!IsCounterMinus)
                            {
                                CorrectCounter++;
                                WrongCharList.Remove(ss);
                                // IsLastWrongChange = true;
                                IsCounterMinus = true;
                                break;
                            }
                        }
                    }

                    correctClickAfter = true;
                    StopCoroutine(StartHandAnim());
                    IsWaitingTimmerStart = false;
                    CorrectPartical.Play();
                    CorrectIMG.gameObject.SetActive(true);
                    CorrectIMG.transform.SetParent(aaa.transform);
                    CorrectIMG.rectTransform.localPosition = Vector3.zero;
                    AlphaCounter++;
                    HandIMG.transform.SetParent(null);
                    HandIMG.gameObject.SetActive(false);
                    IsHandAnim = false;
                    SoundManager.instance.SoundPlay(SoundName.Correct);

                    if (AlphaCounter > AlphaArrayIMG.Length - 1)//AlphaArrayIMG.Length-1
                    {
                        Invoke("WinCondition", 1f);
                        Invoke("WaitPopUpOn", 4f);
                        NextBtnObj.SetActive(false);
                        PlayerPrefs.SetInt("ObjectTicket", 1);
                        GameManagerOwn.instance.ObjTicket.transform.GetChild(1).gameObject.SetActive(false);
                        GameManagerOwn.instance.ObjTicket.transform.GetChild(2).gameObject.SetActive(false);
                    }

                    foreach (GameObject btns in BTNs)
                    {
                        btns.GetComponent<Button>().enabled = false;
                    }

                    NextBtnObj.SetActive(true);
                    BackBtnObj.SetActive(false);
                    NextBtnObj.GetComponent<Button>().interactable = true;
                });
            }
            else
            {

                foreach (string ss in WrongCharList)
                {
                    if (ss == AlphaArrayIMG[AlphaCounter].name)
                    {
                        if (!IsCounterMinus)
                        {
                            CorrectCounter++;
                            WrongCharList.Remove(ss);
                            // IsLastWrongChange = true;
                            IsCounterMinus = true;
                            break;
                        }
                    }
                }

                correctClickAfter = true;
                StopCoroutine(StartHandAnim());
                IsWaitingTimmerStart = false;
                CorrectPartical.Play();
                CorrectIMG.gameObject.SetActive(true);
                CorrectIMG.transform.SetParent(aaa.transform);
                CorrectIMG.rectTransform.localPosition = Vector3.zero;
                AlphaCounter++;
                HandIMG.transform.SetParent(null);
                HandIMG.gameObject.SetActive(false);
                IsHandAnim = false;
                SoundManager.instance.SoundPlay(SoundName.Correct);

                if (AlphaCounter > AlphaArrayIMG.Length - 1)//AlphaArrayIMG.Length-1
                {
                    Invoke("WinCondition", 1f);
                    Invoke("WaitPopUpOn", 4f);
                    NextBtnObj.SetActive(false);
                    PlayerPrefs.SetInt("ObjectTicket", 1);
                    GameManagerOwn.instance.ObjTicket.transform.GetChild(1).gameObject.SetActive(false);
                    GameManagerOwn.instance.ObjTicket.transform.GetChild(2).gameObject.SetActive(false);
                }

                foreach (GameObject btns in BTNs)
                {
                    btns.GetComponent<Button>().enabled = false;
                }

                NextBtnObj.SetActive(true);
                BackBtnObj.SetActive(false);
                NextBtnObj.GetComponent<Button>().interactable = true;
            }
        }
        else
        {
            foreach (string ss in WrongCharList)
            {
                if (ss == AlphaArrayIMG[AlphaCounter].name)
                {
                    isWrongAd = true;
                }
            }

            if (!isWrongAd)
            {
                WrongCharList.Add(AlphaArrayIMG[AlphaCounter].name);
            }

            if (!IsCounterMinus)
            {
                CorrectCounter--;
                IsCounterMinus = true;
            }
            WrongIMG.gameObject.SetActive(true);
            WrongIMG.transform.SetParent(aaa.transform);
            WrongIMG.rectTransform.localPosition = Vector3.zero;
            Waittingtimmer = 6f;

            if (!IsHandAnim)
            {
                IsHandAnim = true;
                StartCoroutine(StartHandAnim());
            }
            SoundManager.instance.SoundPlay(SoundName.Wrong);
            Invoke("TruFalseIconOnOff", 1f);
        }

    }

    void WinCondition()
    {
        if(PlayerPrefs.GetInt("ObjTicket")==0)
        {
            ObjTicket.SetActive(true);
            PlayerPrefs.SetInt("ObjTicket", 1);
        }
        GameManagerOwn.instance.GameComplete();
        ScoreTxt.text = CorrectCounter.ToString();
        SoundManager.instance.SoundPlay(SoundName.win);
        if (CorrectCounter < 20)
        {
            GreetingTxt.text = "GREAT";
            GreetingSoundType = 0;
            starOnCount = 3;
        }
        else if (CorrectCounter >= 20 && CorrectCounter < 26)
        {
            GreetingTxt.text = "FANTASTIC";
            GreetingSoundType = 1;
            starOnCount = 4;
        }
        else
        {
            GreetingTxt.text = "EXCELLENT";
            GreetingSoundType = 2;
            starOnCount = 5;
        }
        for (int i = 0; i < starOnCount; i++)
        {
            Stars[i].SetActive(true);
        }
    }
    void WaitPopUpOn()
    {
        WinPopUp.SetActive(true);
        WinGiftImg.SetActive(true);
        Ballon1Img.SetActive(true);
        Ballon2Img.SetActive(true);
        SoundManager.instance.SoundPlay(SoundName.GreetingSound);

    }

    void TruFalseIconOnOff()
    {
        CorrectIMG.gameObject.SetActive(false);
        CorrectIMG.transform.parent = null;
        WrongIMG.gameObject.SetActive(false);
        WrongIMG.transform.parent = null;
    }

    public void NextBTN()
    {
        correctClickAfter = false;
        CorrectClickAfterTimmer = 0;
        NextBtnObj.GetComponent<Button>().interactable = false;
        IsCounterMinus = false;
        MainAlphaIMG.gameObject.SetActive(false);
        foreach (GameObject Btns in BTNs)
        {
            Btns.SetActive(false);
        }
        MainCamera.transform.DOMoveX(MainCamera.transform.position.x + 22.5f, 1.8f).OnComplete(() =>
         BgMove.instance.BgPossChangeNext()
        );
        Invoke("GenrateMainTxt", 2f);
    }

    public void BackBTN()
    {
        IsHandAnim = false;
        SoundManager.instance.PauseSound_();
        HandIMG.gameObject.SetActive(false);
        AlphaCounter--;
        BackBtnObj.GetComponent<Button>().interactable = false;
        IsCounterMinus = false;
        MainAlphaIMG.gameObject.SetActive(false);
        foreach (GameObject Btns in BTNs)
        {
            Btns.SetActive(false);
        }
        MainCamera.transform.DOMoveX(MainCamera.transform.position.x - 22.5f, 1.8f).OnComplete(() =>
         BgMove.instance.BgPossChangePrevious()
        );
        Invoke("GenrateMainTxt", 2f);
    }
}
