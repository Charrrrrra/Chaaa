using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : CharactorState
{
    
    public WalkState(PlayerController Player, StateController Sc) : base(Player, Sc) {}

    public override void EnterState() {
    }

    public override void ExitState() {
        base.ExitState();
    }

    public override void FrameUpdate() {
        player.GroundMovement();

        if (Input.GetButtonDown("Jump") && player.GroundCheck() && player.can_move) {
            player.sc.ChangeState(player.jumping);
        }
        
        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) {
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
