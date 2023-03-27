using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    float speed;

    void Start()
    {
        speed =  Random.Range(80, 120);
    }

    void Update()
    {
        transform.Rotate(Vector3.forward * -speed * Time.deltaTime);
    }
}
