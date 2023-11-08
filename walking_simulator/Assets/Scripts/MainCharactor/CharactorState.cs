using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorState
{

    public PlayerController player;
    public StateController sc;

    public CharactorState(PlayerController Player, StateController SC) {
        this.player = Player;
        this.sc = SC;
    }

    public virtual void EnterState() {}
    public virtual void ExitState() {}
    public virtual void FrameUpdate() {}

}
