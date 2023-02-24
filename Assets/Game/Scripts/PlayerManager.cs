using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : InputManager
{
    public static PlayerManager instance { get; set; }
    public float PlayerSpeed = 5;
    public AnimationController PlayerAnimControl;

    void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (move)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * PlayerSpeed);
        }
    }

    public override void PlayerSwipe(Vector3 _position)
    {
        transform.position = new Vector3(transform.position.x + _position.x / 100, transform.position.y, transform.position.z);

    }

    public override void Hold(bool tapDown, bool hold, bool tapUp)
    {
        // Get hold true when tap down & Get hold false when tap up
    }
    public override void PlayerDrag(bool hold)
    {
        // Get hold true when tap down & Get hold false when tap up
    }
    public override void DragHold(Vector3 pos)
    {
        transform.position = pos;
    }
    public override void Custom()
    {
        // Your Custom code here
    }


    
}
