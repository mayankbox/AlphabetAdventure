using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDilogu : MonoBehaviour
{
    LinkClass lc = new LinkClass();

    [SerializeField]
    GameObject ExitDiolog;

    [SerializeField]
    GameObject GreatingPopup;

    bool exitOpened;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                if (!exitOpened)
                {
                    exitOpened = true;
                    CloseApplication();
                }
                else
                {
                    exitOpened = false;
                    CloseExitDiolog();
                }
            }
        }
    }

    public void CloseApplication()
    {
       // AdManager.Instance.DestroyBannerAD();
        ExitDiolog.SetActive(true);
    }

    public void RateUs()
    {
        lc.RateUs();

        Invoke("Quit", 0.5f);
    }

    public void Later()
    {
        //AdManager.Instance.ShowInterstitial(() =>
        //{
            Greating();
        //});
    }

    public void Greating()
    {
        GreatingPopup.SetActive(true);

        Invoke("Quit", 1.5f);
    }

    void CloseExitDiolog()
    {
        ExitDiolog.SetActive(false);
    }

    void Quit()
    {
        Application.Quit();
    }
}
