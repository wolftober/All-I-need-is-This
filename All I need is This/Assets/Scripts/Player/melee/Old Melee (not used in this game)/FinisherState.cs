using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinisherState : MeleeBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);
        //Attack
        attackIndex = 3;
        duration = 0.75f;
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
