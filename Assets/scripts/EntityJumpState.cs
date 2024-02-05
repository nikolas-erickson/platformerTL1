using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class EntityJumpState : EntityState
{
    public override void EnterState(Entity entity)
    {
        entity.rigidBody.velocity = new Vector2(entity.rigidBody.velocity.x, entity.jumpPower);
    }
    public override void UpdateState(Entity entity)
    {

    }
    public override void FixedUpdateState(Entity entity)
    {
        entity.rigidBody.velocity = new Vector2(entity.horizontal * entity.speed, entity.rigidBody.velocity.y);

    }
    public override void OnCollisionEnter(Entity entity)
    {
        entity.enterIdleState();
    }

    public override string getState()
    {
        return "jump";
    }
}
