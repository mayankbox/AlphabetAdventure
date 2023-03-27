using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMpve : MonoBehaviour
{
    [SerializeField] float Speed;
    [SerializeField] bool IsMonkey;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * Speed * Time.deltaTime);

        if (transform.localPosition.x > 680f)
        {
            if (IsMonkey)
            {
                transform.localPosition = new Vector2(-5000, transform.localPosition.y);
            }
            else
            {
                transform.localPosition = new Vector2(-1200, transform.localPosition.y);
            }
        }

    }
}
