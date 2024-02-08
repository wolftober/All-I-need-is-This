using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEventHelper : MonoBehaviour
{
    public UnityEvent OnAnimationEventTrigger, OnAttackPerformed;

    public void TriggeredEvent()
    {
        OnAnimationEventTrigger?.Invoke();
    }

    public void TriggerAttack()
    {
        OnAttackPerformed?.Invoke();
    }
}
