using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryState : MeleeBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);
        //Attack
        attackIndex = 1;
        duration = 0.5f;
        animator.SetTrigger("Attack" + attackIndex);
        Debug.Log("Plater Attack " + attackIndex + "Fired!")
;    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if(fixedtime >= duration){
            if(shouldCombo){
                stateMachine.SetNextState(new ComboState());
            }
            else
                stateMachine.SetNextStateToMain();
        }
    }
}
