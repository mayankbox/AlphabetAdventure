using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class splashChage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SplashToGame());
    }

    IEnumerator SplashToGame()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(1);
    }
}
