using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public abstract class EntityState
{
    public enum State
    {
        state_idle,
        state_run,
        state_dead,
        state_recover,
        state_jump
    }
    public abstract void EnterState(Entity entity);
    public abstract void UpdateState(Entity entity);
    public abstract void FixedUpdateState(Entity entity);
    public abstract void OnCollisionEnter(Entity entity);
    public abstract string getState();
}
