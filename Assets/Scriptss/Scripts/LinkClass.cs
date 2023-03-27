using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LinkClass
{
    public string appUrl = "https://play.google.com/store/apps/details?id=com.darkfoxgame.trafficjam.game";

    public void RateUs()
    {
        Application.OpenURL(appUrl);
    }

    public void ContactUs()
    {
        Application.OpenURL("soulappstudio@gmail.com");
    }

    public void Info()
    {
      //  Application.OpenURL("http://elisioninfotech.com");
    }

    public void MoreGames()
    {
        Application.OpenURL("https://play.google.com/store/apps/developer?id=Dark+Fox+Game");
    }

    public void PrivacyPolicy()
    {
        Application.OpenURL("https://soulappstudiopolicy.blogspot.com/p/privacy-policy.html");
    }
}
