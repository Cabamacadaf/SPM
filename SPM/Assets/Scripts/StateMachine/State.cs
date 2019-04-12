using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : ScriptableObject
{
    // Methods
    public virtual void Initialize (StateMachine owner) { }
    public virtual void Enter () { }
    public virtual void Exit () { }
    public virtual void HandleUpdate () { }
    public virtual void HandleCollision (Collision collision) { }
    public virtual void HandleTrigger (Collider collider) { }
}
