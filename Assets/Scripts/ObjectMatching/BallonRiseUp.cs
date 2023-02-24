using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallonRiseUp : MonoBehaviour
{
    [SerializeField] float Speed;
    [SerializeField] bool isMoveDown;

    // Update is called once per frame
    void Update()
    {
        if (isMoveDown)
        {
            transform.Translate(Vector2.down * Speed * Time.deltaTime);

        }
        else
        {
            transform.Translate(Vector2.up * Speed * Time.deltaTime) ;

        }
    }
}
