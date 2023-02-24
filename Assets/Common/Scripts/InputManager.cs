using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public InputType Inputype = InputType.Swipe;

    void FixedUpdate()
    {
        if (GameManager.instance.Gamestate == GameState.Playing)
        {
            switch (Inputype)
            {
                case InputType.Swipe:
                    Swipe();
                    break;
                case InputType.Hold:
                    Hold();
                    break;
                case InputType.Drag:
                    Drag();
                    break;
                case InputType.Drag_N_Drop:
                    Drag_N_Drop();
                    break;
                case InputType.Custom:
                    Custom();
                    break;
            }
        }
    }

    public virtual void PlayerSwipe(Vector3 _position)
    {
    }
    public virtual void Hold(bool tapDown, bool hold, bool tapUp)
    {
    }
    public virtual void PlayerDrag(bool hold)
    {
    }
    public virtual void DragHold(Vector3 pos)
    {
    }
    public virtual void Custom()
    {
    }

    Vector3 startpos;


    void Swipe()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startpos = Input.mousePosition;
            Hold(true, false, false);
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 dif = Input.mousePosition - startpos;

            PlayerSwipe(dif);
            Hold(false, true, false);
            startpos = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            Hold(false, false, true);
        }

    }
    void Hold()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Hold(true, false, false);
        }
        if (Input.GetMouseButton(0))
        {
            Hold(false, true, false);
        }
        if (Input.GetMouseButtonUp(0))
        {
            Hold(false, false, true);
        }
    }

    public bool move = false;
    void Drag()
    {
        //if (move)
        //{
        //    transform.Translate(Vector3.forward * Time.deltaTime * 3.3f);
        //}
        if (Input.GetMouseButtonDown(0))
        {
            move = true;
            startpos = Input.mousePosition;
            PlayerDrag(move);
            Hold(true, false, false);
        }
        if (Input.GetMouseButton(0))
        {

            Vector3 last_posi = Input.mousePosition;
            Vector3 dif = new Vector3(last_posi.x - startpos.x, 0, last_posi.y - startpos.y);
            if (startpos == last_posi)
                return;

            Quaternion player_rot = Quaternion.LookRotation(dif);
            transform.rotation = Quaternion.Slerp(transform.rotation, player_rot, 5);
            Hold(false, true, false);

        }
        if (Input.GetMouseButtonUp(0))
        {
            move = false;
            PlayerDrag(move);
            Hold(false, false, true);
        }
    }
    void Drag_N_Drop()
    {
        if (Input.GetMouseButton(0))
        {
            Plane plane = new Plane(Vector3.up, transform.position);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float point = 0f;

            if (plane.Raycast(ray, out point))
                startpos = ray.GetPoint(point);
            DragHold(startpos);
        }
    }
}
