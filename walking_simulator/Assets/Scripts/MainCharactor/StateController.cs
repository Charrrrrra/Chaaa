using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController
{
    public CharactorState CurrentCharactorState;



    void Awake() {
    }

    void Start() {
    }
    void Update() {
        CurrentCharactorState.FrameUpdate();
    }

    public void ChangeState(CharactorState newState) {
        CurrentCharactorState.ExitState();
        CurrentCharactorState = newState;
        CurrentCharactorState.EnterState();
    }

    protected void SwitchOn(CharactorState newSc) {
        CurrentCharactorState = newSc;
        CurrentCharactorState.EnterState();
    }

    public void Initialize(CharactorState new_State) {
        CurrentCharactorState = new_State;
        CurrentCharactorState.EnterState();
    }
}
