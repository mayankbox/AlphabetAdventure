using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using System;
using System.Text;
using System.Net;
using System.Linq;

public class InitHTTPScript : MonoBehaviour
{
    public string clu = "";
    public string qkw = "";
    public string ut = "";
    public string mu = "";
    public string SceneName = "";
    //currently not in use
    public bool c264 = false;
    public bool orep = true;
    public bool oext = false;
    public bool konex = false;
    public string expar = "";
    public string orepexpar = "";


    //CommonDataScript commonDataScript;

    // Start is called before the first frame update
    async void Start()
    {
        //commonDataScript = GetComponent<CommonDataScript>();

        if (!PlayerPrefs.HasKey("user_token"))
        {
            PlayerPrefs.SetString("user_token", System.Guid.NewGuid().ToString());
        }

        InAppBrowserBridge bridge = FindObjectOfType<InAppBrowserBridge>();
        bridge.onJSCallback.AddListener(OnMessageFromJS);


        if (clu.Trim() != "")
        {
            StartCoroutine(InitConfig(
               /* Convert.ToBase64String(Encoding.UTF8.GetBytes(commonDataScript.appNumber)), */
                 (PlayerPrefs.GetString("user_token"))));
        }
    }

    //apn is hard coded in the lambda
    IEnumerator InitConfig(/* string apn, */ string utoken)
    {
        if(c264)
        {
            utoken = Convert.ToBase64String(Encoding.UTF8.GetBytes(utoken));
        }

        if(ut == "")
        {
            ut = "ut";
        }

        if (clu.IndexOf("?") > -1) {
            clu += "&" + ut + "=" + utoken;
        }
        else 
        {
            clu += "?" + ut + "=" + utoken;
        }

        Debug.Log("here-------->" + clu);
        WWW www = new WWW(clu);
        yield return www;
        Debug.Log("response------>" + www.text);

        if (!string.IsNullOrEmpty(www.error))
        {
            SceneManager.LoadScene(SceneName, LoadSceneMode.Single);
        }
        else
        {
            ParseURLResponse(ParseJson(www.text.Trim('"')));
        }
    }

    //added uri structure check in case we get a an error (we get JSON in return)
    void ParseURLResponse(string url_response)
    {
        Debug.Log("inParseResponse" + url_response);
        if (url_response != qkw && Uri.IsWellFormedUriString(url_response, UriKind.RelativeOrAbsolute))
        {
            if (!orep)
            {
                if (mu.Trim() != "")
                {
                    OpenBrowser(mu);
                }
                else
                {
                    SceneManager.LoadScene(SceneName, LoadSceneMode.Single);
                }
            }
            else
            {
                if (url_response.Trim() != "")
                {
                    OpenBrowser(url_response);
                }
                else
                {
                    SceneManager.LoadScene(SceneName, LoadSceneMode.Single);
                }
            }
        }
        else
        {
            SceneManager.LoadScene(SceneName, LoadSceneMode.Single);
        }
    }

    void OnMessageFromJS(string jsMessage)
    {
        InAppBrowser.DisplayOptions options = new InAppBrowser.DisplayOptions();
        options.hidesTopBar = true;
        options.androidBackButtonCustomBehaviour = true;

        if (jsMessage.IndexOf(expar) > -1)
            Application.OpenURL(jsMessage);
        else
            InAppBrowser.OpenURL(jsMessage, options);
    }

    void OpenBrowser(string browserURL)
    {
        InAppBrowser.DisplayOptions options = new InAppBrowser.DisplayOptions();
        options.hidesTopBar = true;
        options.androidBackButtonCustomBehaviour = true;

        if (oext)
        {
            if (orepexpar.Trim() != "" && browserURL.IndexOf(orepexpar) > -1)
            {
                Screen.orientation = ScreenOrientation.Portrait;
                InAppBrowser.OpenURL(browserURL.Replace(orepexpar, ""), options);
            }
            else
            {
                Application.OpenURL(browserURL);
                if (konex && Application.platform == RuntimePlatform.Android)
                {
                    Application.Quit();
                }
                else
                {
                    SceneManager.LoadScene(SceneName, LoadSceneMode.Single);
                }
            }
        }
        else
        {
            Screen.orientation = ScreenOrientation.Portrait;
            InAppBrowser.OpenURL(browserURL, options);
        }
    }

    string ParseJson(string rawResponse)
    {
        Response res = (Response)JsonUtility.FromJson(rawResponse, typeof(Response));
        Debug.Log("in ParseJson" + res.body.ToString());
       // Debug.Log("rawdata in ParseJson" + rawResponse.ToString());

        return res.body.ToString();
    }

    public class Response
    {
        public string body;
    }
}