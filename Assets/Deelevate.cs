using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deelevate : MonoBehaviour
{
   public void Chaneg()
    {
        AdManager.Instance.ShowInterstitialAd(() => {
            print("CLOSEDDDDD");
        });
    }
}
