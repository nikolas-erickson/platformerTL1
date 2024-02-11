//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEditor.Tilemaps;
//using UnityEditor.UI;
using UnityEngine;

public class Bird : Entity
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask playerLayer;

    private bool isAlive;
    private float removeCorpse;



    // Start is called before the first frame update
    void Start()
    {
        initializeComponents();
        isAlive = true;
        speed = 3;
        jumpPower = 1;
        horizontal = -1;
        removeCorpse = 0.3f;
        enterIdleState();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive)
        {
            removeCorpse -= Time.deltaTime;
            if (removeCorpse <= 0)
            {
                Destroy(gameObject);
            }
        }

        if (isAlive && groundInDirection(Vector2.right * horizontal))
        {
            horizontal *= -1;
            flip();
        }
        currentState.UpdateState(this);
    }


    void FixedUpdate()
    {
        if (!isAlive)
        {
            return;
        }
        currentState.FixedUpdateState(this);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isAlive)
        {
            return;
        }

        if (collision.gameObject.tag == "Player" && hitsObjectInDirection(collision.gameObject, Vector2.up))
        {
            Debug.Log("bird dead");
            kill();
        }
        currentState.OnCollisionEnter(this);
    }



    private bool hitsObjectInDirection(GameObject other, Vector2 dir)
    {
        RaycastHit2D rayCastHit = Physics2D.BoxCast(boxColl2d.bounds.center, boxColl2d.bounds.size,
             0, dir, 0.1f, playerLayer);
        if (rayCastHit.collider == null) return false;
        if (rayCastHit.collider.gameObject == other)
        {
            return true;
        }
        return false;
    }


    private bool groundInDirection(Vector2 dir)
    {
        RaycastHit2D rayCastHit = Physics2D.BoxCast(boxColl2d.bounds.center, boxColl2d.bounds.size,
             0, dir, 0.1f, groundLayer);
        return rayCastHit.collider != null;
    }

    private bool isGroundInFront()
    {
        RaycastHit2D rayCastHit = Physics2D.Raycast(getFrontBottomCorner(),
             Vector2.down, 0.1f, groundLayer);
        return rayCastHit.collider != null;
    }

    private Vector3 getFrontBottomCorner()
    {
        return (transform.position - new Vector3((-horizontal * boxColl2d.bounds.size.x / 2), (boxColl2d.bounds.size.y / 2)));
    }
    private void flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    public bool isVulnerableToJump()
    {
        return true;
    }

    public void kill()
    {
        rigidBody.velocity = Vector2.zero;
        isAlive = false;
        enterDeadState();
    }
}
