using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public float horizontal;
    public Rigidbody2D rigidBody;
    [SerializeField] public float speed;
    [SerializeField] public float jumpPower;
    public EntityState currentState;
    public EntityDeadState deadState = new EntityDeadState();
    public EntityIdleState idleState = new EntityIdleState();
    public EntityJumpState jumpState = new EntityJumpState();
    public EntityRunState runState = new EntityRunState();


    // Start is called before the first frame update
    void Start()
    {
        currentState = idleState;
        currentState.EnterState(this);
    }
}
