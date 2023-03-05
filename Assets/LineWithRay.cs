using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LineWithRay : MonoBehaviour
{
    public LineRenderer LR;
    bool IsRayLine;
    bool IsHitWithBallon;
    RaycastHit2D hit;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
             hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "Ballon")
                {
                    IsRayLine = true;
                    LR.SetPosition(0, hit.transform.position);
                }
            }
        }
        if (IsRayLine)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPosition.z = 0;
            LR.SetPosition(1, worldPosition);

           // hit.collider.gameObject.transform.position = worldPosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit2D hit2 = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit2.collider != null)
            {
                if (hit2.collider.gameObject.tag == "Ballon")
                {
                    if (hit.collider.gameObject != hit2.collider.gameObject)
                    {
                        LR.SetPosition(1, hit2.transform.position);
                        if(hit.collider.gameObject.name==hit2.collider.gameObject.name)
                        {
                            print("HITT___BALLON___TRUE");
                            AlphabetMatching.instance.PlayUpperBallon.Remove
                                (AlphabetMatching.instance.PlayUpperBallon.Where(obj => obj.name == hit.collider.gameObject.name).SingleOrDefault());
                            AlphabetMatching.instance.PlayDownBallon.Remove
                                (AlphabetMatching.instance.PlayDownBallon.Where(obj => obj.name == hit2.collider.gameObject.name).SingleOrDefault());
                            Destroy(hit.collider.gameObject);
                            Destroy(hit2.collider.gameObject);
                            LR.SetPosition(0, Vector3.zero);
                            LR.SetPosition(1, Vector3.zero);
                        }
                        else
                        {
                            print("HITT___BALLON___FALSE");
                            LR.SetPosition(0, Vector3.zero);
                            LR.SetPosition(1, Vector3.zero);
                        }
                    }
                    else
                    {
                        LR.SetPosition(0, Vector3.zero);
                        LR.SetPosition(1, Vector3.zero);
                        print("HITT___WITH___SAMEE__BALLON");

                    }
                    IsRayLine = false;
                }
                else
                {
                    print("HITT___WITH____OTHER__OBJECT");
                    LR.SetPosition(0, Vector3.zero);
                    LR.SetPosition(1, Vector3.zero);
                }
                IsRayLine = false;
            }
            else
            {
                print("NOTT___HITT");
                IsRayLine = false;
                LR.SetPosition(0, Vector3.zero);
                LR.SetPosition(1, Vector3.zero);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
