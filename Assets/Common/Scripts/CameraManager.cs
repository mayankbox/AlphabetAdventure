using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform TargetPlayer;
    public bool IsFollowPlayer = true;
    public Vector3 Offset = Vector3.zero;
   
    void LateUpdate()
    {
        if ((GameManagerOwn.instance.Gamestate == GameState.Playing || GameManagerOwn.instance.Gamestate == GameState.Preparing) && IsFollowPlayer)
        {
            switch (PlayerManager.instance.Inputype)
            {
                case InputType.Swipe:
                    // camera offset === 0,0,-10
                    Swipe();
                    break;
                case InputType.Drag:
                    DragCamerafollow();
                    break;
              
            }
        }

    }


    void DragCamerafollow()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(TargetPlayer.transform.position.x, transform.position.y, TargetPlayer.transform.position.z + Offset.z), Time.deltaTime * 8f);
    }

    void Swipe()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, TargetPlayer.transform.position.z + Offset.z);
    }

 
}
