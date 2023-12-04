using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : CharactorState
{
    public IdleState(PlayerController Player, StateController Sc) : base(Player, Sc) {}

    public override void EnterState() {
        
        Debug.Log("idling");
    }

    public override void ExitState() {
        base.ExitState();
    }

    public override void FrameUpdate() {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) {
            player.sc.ChangeState(player.walking);
        }

        if (Input.GetButtonDown("Jump") && player.GroundCheck() && player.can_move) {
            player.sc.ChangeState(player.jumping);
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
