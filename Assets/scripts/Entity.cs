//using System.Collections;
//using System.Collections.Generic;
//using UnityEditor.Animations;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public float horizontal;
    public Rigidbody2D rigidBody;
    [SerializeField] public float speed;
    [SerializeField] public float jumpPower;
    protected Vector2 facing;
    protected Animator animator;
    protected BoxCollider2D boxColl2d;
    protected EntityState currentState;
    protected EntityDeadState deadState = new EntityDeadState();
    protected EntityIdleState idleState = new EntityIdleState();
    protected EntityJumpState jumpState = new EntityJumpState();
    protected EntityRunState runState = new EntityRunState();
    protected EntityRecoverState recoverState = new EntityRecoverState();


    // Start is called before the first frame update
    protected void initializeComponents()
    {
        animator = GetComponent<Animator>();
        if (animator == null) Debug.LogError("animator reference is null.");
        boxColl2d = GetComponent<BoxCollider2D>();
        if (boxColl2d == null) Debug.LogError("boxColl2d reference is null.");
        rigidBody = GetComponent<Rigidbody2D>();
        if (rigidBody == null) Debug.LogError("rigidBody reference is null.");
        enterIdleState();
    }

    public void enterJumpState()
    {
        currentState = jumpState;
        currentState.EnterState(this);
        animator.SetTrigger("triggerJump");
    }

    public void enterIdleState()
    {
        currentState = idleState;
        currentState.EnterState(this);
        animator.SetTrigger("triggerIdle");
    }

    public void enterRunState()
    {
        currentState = runState;
        currentState.EnterState(this);
        animator.SetTrigger("triggerRun");
    }

    public void enterDeadState()
    {
        currentState = deadState;
        currentState.EnterState(this);
        animator.SetTrigger("triggerDead");
    }

    public void enterRecoverState(float timeToRecover)
    {
        recoverState.recoverTime = timeToRecover;
        currentState = recoverState;
        currentState.EnterState(this);
        animator.SetTrigger("triggerRecover");
    }
}
