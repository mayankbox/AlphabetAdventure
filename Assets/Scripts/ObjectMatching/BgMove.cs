using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgMove : MonoBehaviour
{
    public List<GameObject> BgList = new List<GameObject>();
    public static BgMove instance;

    private void Awake()
    {
        instance = this;
    }
    public void BgPossChangeNext()
    {
        BgList[0].transform.position = new Vector2(BgList[BgList.Count - 1].transform.position.x + 22.5f, 0);
        GameObject FistBg = BgList[0].gameObject;
        BgList.RemoveAt(0);
        BgList.Add(FistBg);
    }
    public void BgPossChangePrevious()
    {
        BgList[BgList.Count - 1].transform.position = new Vector2(BgList[0].transform.position.x - 22.5f, 0);
        GameObject LastBg = BgList[BgList.Count - 1].gameObject;
        BgList.RemoveAt(BgList.Count - 1);
        BgList.Insert(0,LastBg);
    }
}