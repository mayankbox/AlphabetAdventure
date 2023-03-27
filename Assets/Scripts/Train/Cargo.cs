using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cargo : MonoBehaviour
{
    public GameObject ColliderCompartment;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        transform.SetParent(collision.transform);
    }
}