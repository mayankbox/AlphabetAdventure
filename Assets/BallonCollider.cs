using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallonCollider : MonoBehaviour
{
    public bool IsEnter;
    public GameObject CollideObject;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (DragWithRay.instance.HittedObject != null)
        {
            if (DragWithRay.instance.HittedObject.name == collision.gameObject.name)
            {
                IsEnter = true;
                CollideObject = collision.gameObject;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        IsEnter = false;
        CollideObject = null;
    }
}