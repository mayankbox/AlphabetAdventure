using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoRotareZ : MonoBehaviour
{
    [SerializeField] Vector3 Max;
    [SerializeField] Vector3 Min;
    [SerializeField] float speed;
    // Start is called before the first frame update
    private void Start()
    {
        Right();
    }
    void Left()
    {
        transform.DORotate(Max, speed).OnComplete(() =>
        {
            Right();
        });
    }
    void Right()
    {
        transform.DORotate(Min, speed).OnComplete(() =>
        {
            Left();
        });
    }

    
}
