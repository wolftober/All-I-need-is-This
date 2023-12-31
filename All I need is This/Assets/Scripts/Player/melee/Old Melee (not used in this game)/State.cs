using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{ 
    protected float time {get; set;}
    protected float fixedtime {get; set;}
    protected float latetime {get; set;}

    public StateMachine stateMachine;

    public virtual void OnEnter(StateMachine _stateMachine) {
        // Called at the beginning to set initial variables
        stateMachine = _stateMachine;
    }

    public virtual void OnUpdate() {
         // called every frame to update the state
         time += Time.deltaTime;
    }
    public virtual void OnFixedUpdate() {
        // Called at the beginning to set initial variables
        fixedtime += Time.deltaTime;
    }
    public virtual void OnLateUpdate() {
        // Called at the beginning to set initial variables
        latetime += Time.deltaTime;
    }

    public virtual void OnExit() {
        // called at the end to clean up any data
    }


    
    #region Passthrough Methods

    /// <summary>
    /// Removes a gameobject, component, or asset.
    /// </summary>
    /// <param name="obj">The type of Component to retrieve.</param>
    protected static void Destroy(UnityEngine.Object obj)
    {
        UnityEngine.Object.Destroy(obj);
    }

    /// <summary>
    /// Returns the component of type T if the game object has one attached, null if it doesn't.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    protected T GetComponent<T>() where T : Component { return stateMachine.GetComponent<T>(); }

    /// <summary>
    /// Returns the component of Type <paramref name="type"/> if the game object has one attached, null if it doesn't.
    /// </summary>
    /// <param name="type">The type of Component to retrieve.</param>
    /// <returns></returns>
    protected Component GetComponent(System.Type type) { return stateMachine.GetComponent(type); }

    /// <summary>
    /// Returns the component with name <paramref name="type"/> if the game object has one attached, null if it doesn't.
    /// </summary>
    /// <param name="type">The type of Component to retrieve.</param>
    /// <returns></returns>
    protected Component GetComponent(string type) { return stateMachine.GetComponent(type); }
    #endregion
}
