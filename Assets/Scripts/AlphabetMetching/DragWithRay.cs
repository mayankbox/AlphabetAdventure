using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;


public class DragWithRay : MonoBehaviour
{
    public GameObject HittedObject;
    public GameObject PreviousParent;
    //public GameObject NextButton;
    //public GameObject PreviousButton;
    public GameObject stepCompletePanel;
    public GameObject RabbitJoy;
    public bool MouseDowned;
    //public GameObject DogJoy;
    //public GameObject CatJoy;
    //public GameObject RatJoy;

   public float CorrectClickAfterTimmer;
   public bool correctClickAfter;


    int HittedAlphaIndex;
    public static DragWithRay instance;
    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
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


        if (Input.GetMouseButtonDown(0))
        {

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                if (hit.transform.gameObject.tag == "Ballon")
                {
                    // AlphabetMatching.instance.StopHandAnim();
                  //  AlphabetMatching.instance.IsHandAnim = false;
                    AlphabetMatching.instance.IsWaitingTimmerStart = false;
                    AlphabetMatching.instance.HandIMG.gameObject.SetActive(false);

                    HittedObject = hit.transform.gameObject;
                    PreviousParent = hit.transform.parent.gameObject;

                    HittedObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
                    HittedObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 3;

                    if (AlphabetMatching.instance.PlayUpperBallon.Where(obj => obj.gameObject == PreviousParent).SingleOrDefault())
                    {
                        GameObject aa = AlphabetMatching.instance.PlayDownBallon.Where(obj => obj.name == hit.collider.gameObject.name).SingleOrDefault();
                        aa.transform.GetChild(0).GetComponent<Collider2D>().isTrigger = true;
                    }
                    else
                    {
                        GameObject aa = AlphabetMatching.instance.PlayUpperBallon.Where(obj => obj.name == hit.collider.gameObject.name).SingleOrDefault();
                        aa.transform.GetChild(0).GetComponent<Collider2D>().isTrigger = true;
                    }
                }
            }
        }


        if (Input.GetMouseButton(0))
        {
            if (HittedObject != null)
            {
                // AlphabetMatching.instance.StopHandAnim();
               // AlphabetMatching.instance.IsHandAnim = false;
                AlphabetMatching.instance.IsWaitingTimmerStart = false;
                AlphabetMatching.instance.HandIMG.gameObject.SetActive(false);
                MouseDowned = true;

                Vector2 mousePoss = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                HittedObject.transform.position = mousePoss;
            }
        }


        if (Input.GetMouseButtonUp(0))
        {
            if (HittedObject != null)
            {
                if (HittedObject.GetComponent<BallonCollider>().IsEnter == true)
                {
                    AlphabetMatching.instance.StopHandAnim();


                    HittedObject.gameObject.transform.position =
                        HittedObject.GetComponent<BallonCollider>().CollideObject.transform.position;

                    for(int i=0;i<AlphabetMatching.instance.AlphabetTotal.Length;i++)
                    {
                        if(HittedObject.gameObject.name== AlphabetMatching.instance.AlphabetTotal[i].name)
                        {
                            HittedAlphaIndex = i;
                        }
                    }

                    SoundManager.instance.SoundPlay(SoundName.Ballonpop);
                    GameObject aa = Instantiate(AlphabetMatching.instance.SameBallonePartical, HittedObject.gameObject.transform.position, Quaternion.identity);
                    aa.GetComponent<ParticleSystem>().startColor = AlphabetMatching.instance.ColorsOfBallon[HittedAlphaIndex];
                    aa.transform.GetChild(0).GetComponent<ParticleSystem>().startColor = AlphabetMatching.instance.ColorsOfBallon[HittedAlphaIndex];
                    aa.transform.GetChild(1).GetComponent<ParticleSystem>().startColor = AlphabetMatching.instance.ColorsOfBallon[HittedAlphaIndex];
                    aa.transform.GetChild(2).GetComponent<ParticleSystem>().startColor = AlphabetMatching.instance.ColorsOfBallon[HittedAlphaIndex];
                    aa.transform.GetChild(3).GetComponent<ParticleSystem>().startColor = AlphabetMatching.instance.ColorsOfBallon[HittedAlphaIndex];
                    aa.GetComponent<ParticleSystem>().Play();
                    Destroy(aa,1f);
                    AlphabetMatching.instance.PlayUpperBallon.Remove(AlphabetMatching.instance.PlayUpperBallon.Where(obj => obj.name == HittedObject.name).SingleOrDefault());
                    AlphabetMatching.instance.PlayDownBallon.Remove(AlphabetMatching.instance.PlayDownBallon.Where(obj => obj.name == HittedObject.name).SingleOrDefault());
                    Destroy(HittedObject.GetComponent<BallonCollider>().CollideObject.GetComponent<SpriteRenderer>());
                    Destroy(HittedObject.GetComponent<BallonCollider>().CollideObject.transform.GetChild(0).GetComponent<SpriteRenderer>());
                    Destroy(HittedObject.GetComponent<BallonCollider>().CollideObject.transform.GetChild(1).GetComponent<SpriteRenderer>());
                    Destroy(PreviousParent.transform.GetChild(0).GetComponent<SpriteRenderer>());
                    Destroy(PreviousParent.transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>());
                    Destroy(PreviousParent.transform.GetChild(0).transform.GetChild(1).GetComponent<SpriteRenderer>());
                    Destroy(HittedObject.GetComponent<BallonCollider>().CollideObject.transform.parent.gameObject,1f);
                    Destroy(PreviousParent,1f);

                   
                    if (AlphabetMatching.instance.PlayUpperBallon.Count==0)
                    {
                        if (AlphabetMatching.instance.AlphaSeries < 4)///////25
                        {
                            SoundManager.instance.SoundPlay(SoundName.DogCatRatRun);

                            AlphabetMatching.instance.CornerOneAlpha.gameObject.SetActive(false);
                            AlphabetMatching.instance.CornerLastAplha.gameObject.SetActive(false);
                            print("NEXT__STEP");
                            NextBTN();
                            //correctClickAfter = true;

                        }
                        else
                        {
                            SoundManager.instance.SoundPlay(SoundName.Firecraker);
                            AlphabetMatching.instance.IsHandAnim = false;
                            AlphabetMatching.instance.CornerOneAlpha.gameObject.SetActive(false);
                            AlphabetMatching.instance.CornerLastAplha.gameObject.SetActive(false);
                            AlphabetMatching.instance.WinCondition();
                            AlphabetMatching.instance.WaitPopUpOn();
                            PlayerPrefs.SetInt("AlphabetTicket", 1);
                            GameManager.instance.AlphabetTicket.transform.GetChild(1).gameObject.SetActive(false);
                        }
                    }
                }
                else
                {
                    SoundManager.instance.SoundPlay(SoundName.Ballonwrong);
                    HittedObject.gameObject.transform.position = PreviousParent.transform.position;
                    HittedObject.GetComponent<SpriteRenderer>().sortingOrder = 0;
                    HittedObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 1;
                }
            }
            MouseDowned = false;

            // AlphabetMatching.instance.StopHandAnim();
            AlphabetMatching.instance.IsWaitingTimmerStart = true;
            HittedObject = null;
            PreviousParent = null;
        }
    }

   public void NextBTN()
    {
        AlphabetMatching.instance.StopHandAnim();
        AlphabetMatching.instance.IsWaitingTimmerStart = false;
        correctClickAfter = false;
        CorrectClickAfterTimmer = 0;
        AlphabetMatching.instance.RefreshBTN.interactable = false;
        AlphabetMatching.instance.CornerOneAlpha.gameObject.SetActive(false);
        AlphabetMatching.instance.CornerLastAplha.gameObject.SetActive(false);
        foreach (Transform child in AlphabetMatching.instance.ParentOfBallonGenrate.transform)
        {
            Destroy(child.gameObject);
        }
        AlphabetMatching.instance.PlayUpperBallon.Clear();
        AlphabetMatching.instance.PlayDownBallon.Clear();



      //  NextButton.SetActive(false);
      //  PreviousButton.SetActive(false);
        correctClickAfter = false;
        CorrectClickAfterTimmer = 0;
        stepCompletePanel.SetActive(true);


        //DogJoy.transform.position = new Vector2(-22.5f, -1.7f);
        //DogJoy.transform.localScale = new Vector3(1, 1, 1);

        //CatJoy.transform.position = new Vector2(-17.5f, -2f);
        //CatJoy.transform.localScale = new Vector3(1, 1, 1);

        //RatJoy.transform.position = new Vector2(-13f, -2.5f);
        //RatJoy.transform.localScale = new Vector3(1, 1, 1);


        //CatJoy.transform.DOMoveX(17.5f, 7f);
        //RatJoy.transform.DOMoveX(22.5f, 7f);


        //DogJoy.transform.DOMoveX(13, 7f).OnComplete(() =>
        // {
        //     if (AlphabetMatching.instance.AlphaSeries < 25)
        //     {
        //         DogJoy.transform.position = new Vector2(-22.5f, -1.7f);
        //         CatJoy.transform.position = new Vector2(-17.5f, -2f);
        //         CatJoy.transform.position = new Vector2(-13f, -2.5f);
        //         stepCompletePanel.SetActive(false);
        //         AlphabetMatching.instance.StopHandAnim();
        //        // NextButton.SetActive(true);
        //        // PreviousButton.SetActive(true);
        //         StartCoroutine(AlphabetMatching.instance.SetAlphaBallone());
        //     }
        // });

        RabbitJoy.transform.DOScale(Vector3.one, 1.5f);
        
        RabbitJoy.transform.DOMoveX(RabbitJoy.transform.position.x, 3.5f).OnComplete(() =>
         {
             RabbitJoy.transform.DOScale(Vector3.zero, 1.5f).OnComplete(() =>
             {
                 if (AlphabetMatching.instance.AlphaSeries < 25)
                 {
                     //DogJoy.transform.position = new Vector2(-22.5f, -1.7f);
                     //CatJoy.transform.position = new Vector2(-17.5f, -2f);
                     //CatJoy.transform.position = new Vector2(-13f, -2.5f);
                     stepCompletePanel.SetActive(false);
                     AlphabetMatching.instance.StopHandAnim();
                     // NextButton.SetActive(true);
                     // PreviousButton.SetActive(true);
                     StartCoroutine(AlphabetMatching.instance.SetAlphaBallone());
                 }

             });

            
         });
    }

    public void PreviousBTN()
    {
        AlphabetMatching.instance.StopHandAnim();

        correctClickAfter = false;
        CorrectClickAfterTimmer = 0;
        AlphabetMatching.instance.CornerOneAlpha.gameObject.SetActive(false);
        AlphabetMatching.instance.CornerLastAplha.gameObject.SetActive(false);
        foreach (Transform child in AlphabetMatching.instance.ParentOfBallonGenrate.transform)
        {
            Destroy(child.gameObject);
        }
        AlphabetMatching.instance.PlayUpperBallon.Clear();
        AlphabetMatching.instance.PlayDownBallon.Clear();



       // NextButton.SetActive(false);
       // PreviousButton.SetActive(false);
        correctClickAfter = false;
        CorrectClickAfterTimmer = 0;
        stepCompletePanel.SetActive(true);


        //DogJoy.transform.position = new Vector2(22.5f, -1.7f);
        //DogJoy.transform.localScale = new Vector3(-1, 1, 1);
        
        //CatJoy.transform.position = new Vector2(17.5f, -2f);
        //CatJoy.transform.localScale = new Vector3(-1, 1, 1);
        
        //RatJoy.transform.position = new Vector2(13f, -2.5f);
        //RatJoy.transform.localScale = new Vector3(-1, 1, 1);


        //CatJoy.transform.DOMoveX(-17.5f, 7f);
        //RatJoy.transform.DOMoveX(-22.5f, 7f);


        //DogJoy.transform.DOMoveX(-13, 7f).OnComplete(() =>
        //{
        //    if (AlphabetMatching.instance.AlphaSeries < 25)
        //    {
        //        AlphabetMatching.instance.AlphaSeries -= 10;
        //        DogJoy.transform.position = new Vector2(22.5f, -1.7f);
        //        CatJoy.transform.position = new Vector2(17.5f, -2f);
        //        RatJoy.transform.position = new Vector2(13f, -2.5f);

        //        stepCompletePanel.SetActive(false);
        //        AlphabetMatching.instance.StopHandAnim();
        //        //if(AlphabetMatching.instance.AlphaSeries>5)
        //        //{
        //        //    PreviousButton.SetActive(true);
        //        //}
        //        StartCoroutine(AlphabetMatching.instance.SetAlphaBallone());

        //    }
        //});
        
        
        RabbitJoy.transform.DOMoveX(transform.position.x, 7f).OnComplete(() =>
        {
            if (AlphabetMatching.instance.AlphaSeries < 25)
            {
                AlphabetMatching.instance.AlphaSeries -= 10;
                //DogJoy.transform.position = new Vector2(22.5f, -1.7f);
                //CatJoy.transform.position = new Vector2(17.5f, -2f);
                //RatJoy.transform.position = new Vector2(13f, -2.5f);

                stepCompletePanel.SetActive(false);
                AlphabetMatching.instance.StopHandAnim();
                //if(AlphabetMatching.instance.AlphaSeries>5)
                //{
                //    PreviousButton.SetActive(true);
                //}
                StartCoroutine(AlphabetMatching.instance.SetAlphaBallone());

            }
        });
    }
}