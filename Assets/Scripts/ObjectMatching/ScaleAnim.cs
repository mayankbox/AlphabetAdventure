using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScaleAnim : MonoBehaviour
{
    public float Min;
    public float Max;
    public float Duration;
    // Start is called before the first frame update

    private void Start()
    {
        AnimStart();
    }
    void AnimStart()
    {
        Vector3 aa = new Vector3(Min, Min, 1);
        transform.DOScale(aa, Duration).OnComplete(() =>
        {
            AnimEnd();
        });
    }

    void AnimEnd()
    {
        Vector3 aa = new Vector3(Max, Max, 1);
        transform.DOScale(aa, Duration).OnComplete(() =>
        {
            AnimStart();
        });
    }
}
