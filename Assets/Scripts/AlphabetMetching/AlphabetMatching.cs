using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;


public class AlphabetMatching : MonoBehaviour
{
    public GameObject BallonPrefabs;
    public Vector2 BallonUpPoss;
    public Vector2 BallonDownPoss;
    public GameObject ParentOfBallonGenrate;
    List<Sprite> UsedBallonSprite = new List<Sprite>();
    List<Sprite> UsedAlphaSprite = new List<Sprite>();
    public Sprite[] BallonsTotal;
    public Sprite[] AlphabetTotal;
    public Color[] ColorsOfBallon;
    public List<GameObject> PlayUpperBallon = new List<GameObject>();
    public List<GameObject> PlayDownBallon = new List<GameObject>();
   public int AlphaSeries;
    public Button RefreshBTN;


    public GameObject HandIMG;
    [SerializeField] float HandMoveSpeed;
   public bool IsHandAnim;
    public float Waittingtimmer;
   public bool IsWaitingTimmerStart;


    public Sprite[] CornerAlphaTotal;
    public SpriteRenderer CornerOneAlpha;
    public SpriteRenderer CornerLastAplha;


    public GameObject SameBallonePartical;

    [Header("Win")]
    [SerializeField] GameObject AlphaTicket;
    [SerializeField] GameObject WinPopUp;
    [SerializeField] GameObject WinGiftImg;
    [SerializeField] GameObject Ballon1Img;
    [SerializeField] GameObject Ballon2Img;
    bool IsCounterMinus;
    public int GreetingSoundType;

    public static AlphabetMatching instance;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
       StartCoroutine( SetAlphaBallone());
    }

    private void Update()
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
    }
    public IEnumerator SetAlphaBallone()
    {
       


        if (AlphaSeries < 20)
        {
            BallonUpPoss = new Vector2(-8.25f, 2f);
            BallonDownPoss = new Vector2(-8.25f, -2f);

                       
            if (AlphaSeries == 0)
            {
                 IsWaitingTimmerStart = true;
                Invoke("HandTrueStart", 4f);
            }



            for (int i = 0; i < 5; i++)
            {
                BallonUpPoss = new Vector2(BallonUpPoss.x + 2.75f, BallonUpPoss.y);
                GameObject GenratedBallon = Instantiate(BallonPrefabs, BallonUpPoss, Quaternion.identity, ParentOfBallonGenrate.transform);
                GenratedBallon.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = BallonsTotal[AlphaSeries];
                UsedBallonSprite.Add(BallonsTotal[AlphaSeries]);
                GenratedBallon.transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = AlphabetTotal[AlphaSeries];
                UsedAlphaSprite.Add(AlphabetTotal[AlphaSeries]);
                GenratedBallon.name = AlphabetTotal[AlphaSeries].name;
                GenratedBallon.transform.GetChild(0).name = AlphabetTotal[AlphaSeries].name;
                PlayUpperBallon.Add(GenratedBallon);
                AlphaSeries++;
                yield return new WaitForSeconds(0.2f);
            }
            AlphabetMatching.instance.CornerOneAlpha.gameObject.SetActive(true);
            AlphabetMatching.instance.CornerLastAplha.gameObject.SetActive(true);
            CornerLastAplha.sprite = CornerAlphaTotal[AlphaSeries - 1];
            CornerOneAlpha.sprite = CornerAlphaTotal[AlphaSeries - 5];

            //Invoke("NextButtonShow", 2f);
            //if (AlphaSeries > 6)
            //{
            //    Invoke("PreviousButtonShow", 2f); 
            //}


            yield return new WaitForSeconds(0.2f);
            for (int i = 0; i < 5; i++)
            {
                BallonDownPoss = new Vector2(BallonDownPoss.x + 2.75f, BallonDownPoss.y);
                GameObject GenratedBallon = Instantiate(BallonPrefabs, BallonDownPoss, Quaternion.identity, ParentOfBallonGenrate.transform);
                int RandmBallonNum = Random.Range(0, UsedBallonSprite.Count);
                GenratedBallon.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = UsedBallonSprite[RandmBallonNum];

                GenratedBallon.transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = UsedAlphaSprite[RandmBallonNum];
                GenratedBallon.name = UsedAlphaSprite[RandmBallonNum].name;
                GenratedBallon.transform.GetChild(0).name = UsedAlphaSprite[RandmBallonNum].name;
                PlayDownBallon.Add(GenratedBallon);
                UsedBallonSprite.RemoveAt(RandmBallonNum);
                UsedAlphaSprite.RemoveAt(RandmBallonNum);
                yield return new WaitForSeconds(0.2f);
            }
            yield return new WaitForSeconds(1.5f);
                 IsWaitingTimmerStart = true;
            Waittingtimmer = 6f;

            RefreshBTN.interactable = true;
        }

        else
        {
            BallonUpPoss = new Vector2(-9.62f, 2f);
            BallonDownPoss = new Vector2(-9.62f, -2f);

            for (int i = 0; i < 6; i++)
            {
                BallonUpPoss = new Vector2(BallonUpPoss.x + 2.75f, BallonUpPoss.y);
                GameObject GenratedBallon = Instantiate(BallonPrefabs, BallonUpPoss, Quaternion.identity, ParentOfBallonGenrate.transform);
                GenratedBallon.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = BallonsTotal[AlphaSeries];
                UsedBallonSprite.Add(BallonsTotal[AlphaSeries]);
                GenratedBallon.transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = AlphabetTotal[AlphaSeries];
                UsedAlphaSprite.Add(AlphabetTotal[AlphaSeries]);
                GenratedBallon.name = AlphabetTotal[AlphaSeries].name;
                GenratedBallon.transform.GetChild(0).name = AlphabetTotal[AlphaSeries].name;
                PlayUpperBallon.Add(GenratedBallon);

                AlphaSeries++;
                yield return new WaitForSeconds(0.2f);
            }
            AlphabetMatching.instance.CornerOneAlpha.gameObject.SetActive(true);
            AlphabetMatching.instance.CornerLastAplha.gameObject.SetActive(true);
            CornerLastAplha.sprite = CornerAlphaTotal[AlphaSeries-1];
            CornerOneAlpha.sprite = CornerAlphaTotal[AlphaSeries-6];
          //  DragWithRay.instance.NextButton.SetActive(false);
           // Invoke("PreviousButtonShow", 2f);

            yield return new WaitForSeconds(0.2f);
            for (int i = 0; i < 6; i++)
            {
                BallonDownPoss = new Vector2(BallonDownPoss.x + 2.75f, BallonDownPoss.y);
                GameObject GenratedBallon = Instantiate(BallonPrefabs, BallonDownPoss, Quaternion.identity, ParentOfBallonGenrate.transform);
                int RandmBallonNum = Random.Range(0, UsedBallonSprite.Count);
                GenratedBallon.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = UsedBallonSprite[RandmBallonNum];

                GenratedBallon.transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = UsedAlphaSprite[RandmBallonNum];
                GenratedBallon.name = UsedAlphaSprite[RandmBallonNum].name;
                GenratedBallon.transform.GetChild(0).name = UsedAlphaSprite[RandmBallonNum].name;
                PlayDownBallon.Add(GenratedBallon);
                UsedBallonSprite.RemoveAt(RandmBallonNum);
                UsedAlphaSprite.RemoveAt(RandmBallonNum);
                yield return new WaitForSeconds(0.2f);
            }
            yield return new WaitForSeconds(1f);
            IsWaitingTimmerStart = true;
            Waittingtimmer = 6f;

            RefreshBTN.interactable = true;

        }
    }

    //public void NextButtonShow()
    //{
    //    DragWithRay.instance.NextButton.SetActive(true);
    //}
    //public void PreviousButtonShow()
    //{
    //    DragWithRay.instance.PreviousButton.SetActive(true);
    //}
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
            if (DragWithRay.instance.MouseDowned==false)
            {
                HandIMG.gameObject.SetActive(true);
            }
            HandIMG.transform.position = PlayUpperBallon[0].transform.position;
            GameObject aa = PlayDownBallon.Where(obj => obj.name == PlayUpperBallon[0].name).SingleOrDefault();
            HandIMG.transform.DOMove(aa.transform.position, HandMoveSpeed);
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
        Waittingtimmer = 6f;
        IsHandAnim = false;
        IsWaitingTimmerStart = false;
        HandIMG.gameObject.SetActive(false);
    }

    public void Refresh()
    {
        StopHandAnim();
        IsWaitingTimmerStart = false;

        RefreshBTN.interactable = false;
       DragWithRay.instance.correctClickAfter = false;
        DragWithRay.instance.CorrectClickAfterTimmer = 0;
        foreach (Transform child in ParentOfBallonGenrate.transform)
        {
            Destroy(child.gameObject);
        }
        PlayUpperBallon.Clear();
        PlayDownBallon.Clear();

        if (AlphaSeries < 20)
        {
            AlphaSeries -= 5;
        }
        else
        {
            AlphaSeries -= 6;

        }
        StartCoroutine(SetAlphaBallone());
    }
   public void WinCondition()
    {
        Invoke("WaitWin", 1f);
    }
     void WaitWin()
    {
        if(PlayerPrefs.GetInt("AlphaTicket")==0)
        {
            AlphaTicket.SetActive(true);
            PlayerPrefs.SetInt("AlphaTicket", 1);
        }
        GameManagerOwn.instance.GameComplete();
        GreetingSoundType = 2;
        SoundManager.instance.SoundPlay(SoundName.win);
    }
   public void WaitPopUpOn()
    {
        Invoke("WaitPopOn", 4f);

    }
    void WaitPopOn()
    {
        WinPopUp.SetActive(true);
        WinGiftImg.SetActive(true);
        Ballon1Img.SetActive(true);
        Ballon2Img.SetActive(true);
        GreetingSoundType = 2;
        SoundManager.instance.SoundPlay(SoundName.GreetingSoundAlphabetMatch);
    }
}
