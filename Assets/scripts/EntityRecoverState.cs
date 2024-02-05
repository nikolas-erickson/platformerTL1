using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class EntityRecoverState : EntityState
{
    public float recoverTime;
    public override void EnterState(Entity entity)
    {
    }
    public override void UpdateState(Entity entity)
    {
        recoverTime -= Time.deltaTime;
        if (recoverTime <= 0)
        {
            entity.enterIdleState();
        }
    }
    public override void FixedUpdateState(Entity entity)
    {

    }
    public override void OnCollisionEnter(Entity entity)
    {

    }
    public override string getState()
    {
        return "recover";
    }
}