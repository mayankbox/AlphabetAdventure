using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float speed;

    public bool IsStrat;

    void Update()
    {
        if(!IsStrat)
        transform.Rotate(0, 0, speed);
    }
}