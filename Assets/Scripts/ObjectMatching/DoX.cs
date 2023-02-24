using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoX : MonoBehaviour
{
    public bool StopMoving;
    public bool IsLocal;
    public bool isX;
    public bool isY;
    public float MinX;
    public float MaxX;
    public float MinY;
    public float MaxY;
    public int speed;

    // Start is called before the first frame update
    void Start()
    {
       // speed = Random.Range(3, 8);
        UpMove();
    }

    void UpMove()
    {
        if (StopMoving != true)
        {
            if (isX == true)
            {
                if (IsLocal)
                {
                    transform.DOLocalMoveX(MaxX, speed).OnComplete(() =>
                    {
                        DownMove();
                    }).SetId(DOTweenIDs.Example1);
                }
                else
                {
                    transform.DOMoveX(MaxX, speed).OnComplete(() =>
                    {
                        DownMove();
                    }).SetId(DOTweenIDs.Example1);
                }
            }
            if (isY == true)
            {
                if (IsLocal)
                {
                    transform.DOLocalMoveX(MaxY, speed).OnComplete(() =>
                    {
                        DownMove();
                    }).SetId(DOTweenIDs.Example1);
                }
                else
                {
                    transform.DOMoveY(MaxY, speed).OnComplete(() =>
                    {
                        DownMove();
                    }).SetId(DOTweenIDs.Example1);
                }
            }
        }
    }

    void DownMove()
    {
        if (StopMoving != true)
        {
            if (isX == true)
            {
                if (IsLocal)
                {
                    transform.DOLocalMoveX(MinX, speed).OnComplete(() =>
                    {
                        UpMove();
                    }).SetId(DOTweenIDs.Example2);
                }
                else
                {
                    transform.DOMoveX(MinX, speed).OnComplete(() =>
                    {
                        UpMove();
                    }).SetId(DOTweenIDs.Example2);
                }
            }
            if (isY == true)
            {
                if (IsLocal)
                {
                    transform.DOLocalMoveX(MinY, speed).OnComplete(() =>
                    {
                        UpMove();
                    }).SetId(DOTweenIDs.Example2);
                }
                else
                {
                    transform.DOMoveY(MinY, speed).OnComplete(() =>
                    {
                        UpMove();
                    }).SetId(DOTweenIDs.Example2);
                }
            }
        }
    }
}