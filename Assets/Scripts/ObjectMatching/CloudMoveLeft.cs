using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMoveLeft : MonoBehaviour
{
    [SerializeField] float Speed;
    [SerializeField] bool isMoveLeft;
    [SerializeField] GameObject MainCamera;

    void Update()
    {
        if(isMoveLeft)
        {
            transform.Translate(Vector2.left * Speed*Time.deltaTime);
            if(transform.position.x<=MainCamera.transform.position.x-12)
            {
                transform.position = new Vector2(MainCamera.transform.position.x + 12, transform.position.y);
            }
        }
        else
        {
            transform.Translate(Vector2.right * Speed * Time.deltaTime);
            if (transform.position.x >= MainCamera.transform.position.x + 12)
            {
                transform.position = new Vector2(MainCamera.transform.position.x - 12, transform.position.y);
            }
        }
    }
}