using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboState : MeleeBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);
        //Attack
        attackIndex = 2;
        duration = 0.5f;
        animator.SetTrigger("Attack" + attackIndex);
        Debug.Log("Plater Attack " + attackIndex + "Fired!")
;    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if(fixedtime >= duration){
            if(shouldCombo){
                stateMachine.SetNextState(new FinisherState());
            }
            else
                stateMachine.SetNextStateToMain();
        }
    }
}
