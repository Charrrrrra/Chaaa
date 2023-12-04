using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : CharactorState
{
    public JumpState(PlayerController Player, StateController Sc) : base(Player, Sc) {}

    public override void EnterState() {
        base.EnterState();
        
        Debug.Log("Jumping");
    }

    public override void ExitState() {
        base.ExitState();
    }

    public override void FrameUpdate() {
        player.Jump();
        if (!Input.GetButtonDown("Jump") || !player.GroundCheck() || !player.can_move) {
            player.sc.ChangeState(player.idling);
        }
            
        if (player.can_move) {
            player.SwitchAnim();
        }
        else {
            player.sc.ChangeState(player.leaving);
            player.anim.SetBool("walkout", true);
        }
    }
}
