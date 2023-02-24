using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator PlayerAnim;

    public void SetPlayerTrigger(PlayerAnimState animstate)
    {
        PlayerAnim.SetTrigger(animstate.ToString());
    }
}
