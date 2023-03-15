using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TrainManager : MonoBehaviour
{
    public bool IsTrainMove;
    public int CargoDepartmentNum;
    public bool IsClickable;

    public Sprite[] Alphabet;
    public Sprite[] AlphabetObj;
   public int AlphabetWiseCounter;

    RaycastHit2D hit;
    public GameObject TrainEngineMain;
    bool IsFirstTime;
    public static TrainManager instance;
    public RotateObject[] Wheels;

    public GameObject CargoPrefabs;
    GameObject GenratedCargo;
    public GameObject HookCrane;
    public GameObject UpperCranePart;
   public bool CargoFallDown;
    public Animator CargoLineAnim;

    public GameObject HandIMG;
    bool IsHandAnim;
    public float Waittingtimmer;
    public bool IsWaitingTimmerStart;
    bool IsfirstTimeHint;
    bool IsFisrtTimeCraneMove;


    public GameObject RedLight;
    public GameObject GreenLight;

    public Sprite TransparanetBOx;

    [Header("Win")]
    [SerializeField] GameObject WinPopUp;
    //[SerializeField] GameObject WinGiftImg;
    //[SerializeField] GameObject Ballon1Img;
    //[SerializeField] GameObject Ballon2Img;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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

        if (GameManager.instance.gametype == GameType.Train)
        {
            //if (IsClickable == true)
            //{
            //    GenratedCargo.GetComponent<Rigidbody2D>().isKinematic = false;
            //    Destroy(GenratedCargo.transform.GetChild(2).gameObject, 0.8f);
            //    GenratedCargo.transform.parent = null;
            //    CargoDepartmentNum++;
            //    StartCoroutine(WaitCargoHitSoundPlay());

            //    if (!IsfirstTimeHint)
            //    {
            //        IsWaitingTimmerStart = true;
            //        Invoke("HandTrueStart", 1f);
            //        IsfirstTimeHint = true;
            //    }
            //    else
            //    {
            //        IsWaitingTimmerStart = true;

            //    }
            //    IsClickable = false;
            //}


            if (Input.GetMouseButtonDown(0))
            {

                hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.collider != null)
                {

                    if (hit.collider.tag == "CargoBox" && CargoFallDown)
                    {
                        CargoFallDown = false;
                        if (CargoDepartmentNum == 3)
                        {
                            StartCoroutine(TrainMove());
                            CargoDepartmentNum = 0;
                        }
                        else
                        {
                            if (AlphabetWiseCounter < 26)////////26
                            {
                                StartCoroutine(CargoGenrator());
                            }
                            else
                            {
                                print("------------------WINNN----------------------GAMEE------------------------");
                                WaitPopOn();
                                WinCondition();
                            }
                        }
                        StopHandAnim();
                        IsWaitingTimmerStart = false;
                        SoundManager.instance.SoundPlay(SoundName.forAlphaTrain);
                        if (GameConfige.Music != 0)
                        {
                            CanvasManager.instance.MusicAudiosource.volume = 0.1f;
                        }
                        hit.collider.transform.GetChild(2).GetComponent<ParticleSystem>().Play();
                        //hit.collider.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = hit.collider.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite;
                        //hit.collider.transform.fGetChild(1).transform.DOMoveY(hit.collider.transform.GetChild(1).transform.position.y + 2.5f, 1f);
                        Invoke("objLayerShort", 0.5f);
                         hit.collider.transform.GetChild(0).gameObject.SetActive(false);
                        hit.collider.GetComponent<SpriteRenderer>().sprite = TransparanetBOx;
                        hit.collider.gameObject.tag = "Untagged";

                    }
                }


            }
        }
    }

    void objLayerShort()
    {
        hit.collider.transform.GetChild(1).GetComponent<SpriteRenderer>().sortingOrder = 6;

    }

    IEnumerator WaitCargoHitSoundPlay()
    {
        yield return new WaitForSeconds(0.4f);
        SoundManager.instance.SoundPlay(SoundName.CargoHit);

        //hit.collider.gameObject.tag = "CargoBox";
    }
    IEnumerator WaitCargoFallDownTrue()
    {
        yield return new WaitForSeconds(0.82f);
        CargoFallDown = true;

    }
    public IEnumerator TrainMove()
    {
        if (!IsFirstTime)
        {
            GreenLight.SetActive(true);
            RedLight.SetActive(false);
            SoundManager.instance.SoundPlay(SoundName.TrainRunning);
            if (GameConfige.Music != 0)
            {
                CanvasManager.instance.MusicAudiosource.volume = 0.1f;
            }
            WheeleTurnOn();
            TrainEngineMain.transform.DOMoveX(-18f, 6f).OnComplete(() =>
            {
                WheeleTurnOff();
                if (GameConfige.Music != 0)
                {
                    CanvasManager.instance.MusicAudiosource.volume = 0.3f;
                }
                GreenLight.SetActive(false);
                RedLight.SetActive(true);
                StartCoroutine(CargoGenrator());
            });
            IsFirstTime = true;
        }
        else
        {
            yield return new WaitForSeconds(2.3f);
            GreenLight.SetActive(true);
            RedLight.SetActive(false);
            SoundManager.instance.SoundPlay(SoundName.TrainRunning);
            if (GameConfige.Music != 0)
            {
                CanvasManager.instance.MusicAudiosource.volume = 0.1f;
            }
            WheeleTurnOn();
            TrainEngineMain.transform.DOMoveX(TrainEngineMain.transform.position.x - 21.5f, 5.5f).OnComplete(() =>
            {
                WheeleTurnOff();
                    StartCoroutine(CargoGenrator());
                if (GameConfige.Music != 0)
                {
                    CanvasManager.instance.MusicAudiosource.volume = 0.3f;
                }
                GreenLight.SetActive(false);
                RedLight.SetActive(true);
            });
        }
    }
    void WheeleTurnOn()
    {
        foreach (RotateObject rot in Wheels)
        {
            rot.IsStrat = false;
        }
    }
    
    void WheeleTurnOff()
    {
        foreach (RotateObject rot in Wheels)
        {
            rot.IsStrat = true;
        }
    }
    
    public IEnumerator CargoGenrator()
    {
        if (CargoDepartmentNum==0)
        {
            yield return new WaitForSeconds(0.5f);
            IsFisrtTimeCraneMove = true;
            if (GameConfige.Music != 0)
            {
                CanvasManager.instance.MusicAudiosource.volume = 0.3f;
            }
        }
        else
        {
            yield return new WaitForSeconds(2f);
            if (GameConfige.Music != 0)
            {
                CanvasManager.instance.MusicAudiosource.volume = 0.3f;
            }
        }
        print(CargoDepartmentNum);
        switch(CargoDepartmentNum)
        {
            case 0:
                CargoLineAnim.Play("UpperCraneLine");

                break;
            case 1:
                CargoLineAnim.Play("UpperCraneLine2");

                break;
            case 2:
                CargoLineAnim.Play("UpperCraneLine3");

                break;

        }
                SoundManager.instance.SoundPlay(SoundName.CraneMoving);
       // yield return new WaitForSeconds(2f);
        yield return new WaitForSeconds(1.5f);
        GenratedCargo = Instantiate(CargoPrefabs, HookCrane.transform.position, Quaternion.identity, HookCrane.transform);
        GenratedCargo.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Alphabet[AlphabetWiseCounter];
        GenratedCargo.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = AlphabetObj[AlphabetWiseCounter];
        AlphabetWiseCounter++;
        yield return new WaitForSeconds(0.2f);

        IsClickable = true;


        GenratedCargo.GetComponent<Rigidbody2D>().isKinematic = false;
        Destroy(GenratedCargo.transform.GetChild(2).gameObject, 0.8f);
        GenratedCargo.transform.parent = null;
        CargoDepartmentNum++;
        StartCoroutine(WaitCargoHitSoundPlay());
        StartCoroutine(WaitCargoFallDownTrue());

        if (!IsfirstTimeHint)
        {
            IsWaitingTimmerStart = true;
            Invoke("HandTrueStart", 1f);
            IsfirstTimeHint = true;
        }
        else
        {
            IsWaitingTimmerStart = true;

        }
        IsClickable = false;
    }

    public void WinCondition()
    {
        Invoke("WaitWin", 2.3f);
    }
    void WaitWin()
    {
        GameManager.instance.GameComplete();
        SoundManager.instance.SoundPlay(SoundName.win);
    }
    public void WaitPopUpOn()
    {
        Invoke("WaitPopOn", 4f);

    }
    void WaitPopOn()
    {
        WinPopUp.SetActive(true);
        //WinGiftImg.SetActive(true);
        //Ballon1Img.SetActive(true);
        //Ballon2Img.SetActive(true);
        //GreetingSoundType = 2;
      //  SoundManager.instance.SoundPlay(SoundName.GreetingSoundAlphabetMatch);
    }

  
    void HandTrueStart()
    {
        IsHandAnim = true;
        StartCoroutine(StartHandAnim());
    }
    public IEnumerator StartHandAnim()
    {
        yield return new WaitForSeconds(0.2f);
        if (IsHandAnim)
        {

                HandIMG.gameObject.SetActive(true);
            HandIMG.transform.position = new Vector3( GenratedCargo.transform.position.x, GenratedCargo.transform.position.y-2f,0f);
            Invoke("AgainHandAnim", 2f);
        }
    }
    void AgainHandAnim()
    {
        if (IsHandAnim)
            StartCoroutine(StartHandAnim());
    }

    public void StopHandAnim()
    {
        StopCoroutine(StartHandAnim());
        Waittingtimmer = 7f;
        IsHandAnim = false;
        IsWaitingTimmerStart = false;
        HandIMG.gameObject.SetActive(false);
    }

}
