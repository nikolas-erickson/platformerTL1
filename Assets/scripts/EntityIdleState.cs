using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class EntityIdleState : EntityState
{
    public override void EnterState(Entity entity)
    {
    }
    public override void UpdateState(Entity entity)
    {
        if (entity.horizontal != 0)
        {
            entity.enterRunState();
        }
    }
    public override void FixedUpdateState(Entity entity)
    {
        entity.rigidBody.velocity = new Vector2(entity.horizontal * entity.speed, entity.rigidBody.velocity.y);

    }
    public override void OnCollisionEnter(Entity entity)
    {

    }
    public override string getState()
    {
        return "idle";
    }
}

