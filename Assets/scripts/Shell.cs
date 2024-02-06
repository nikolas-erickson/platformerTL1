using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : Entity
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask playerLayer;
    private bool _hitWall;
    private bool _charging;
    // Start is called before the first frame update
    void Start()
    {
        initializeComponents();
        speed = 3;
        jumpPower = 1;
        horizontal = -1;
        enterIdleState();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && hitsObjectInDirection(collision.gameObject, Vector2.up, playerLayer))
        {
            enterIdleState();
            horizontal = 0;
        }

        if (collision.gameObject.tag == "Player" && hitsObjectInDirection(collision.gameObject, Vector2.left, playerLayer))
        {
            enterRunState();
            horizontal = 0;
        }
        else if (collision.gameObject.tag == "Player" && hitsObjectInDirection(collision.gameObject, Vector2.right, playerLayer))
        {
            enterRunState();
            horizontal = 0;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("ground") &&
            hitsObjectInDirection(collision.gameObject, Vector2.right * horizontal, groundLayer))
        {
            if (_charging)
            {
                //hitwall
                enterRecoverState(0.5f);
                _charging = false;
                flip();
                horizontal = 0;
            }
        }
        currentState.OnCollisionEnter(this);
    }


    private bool hitsObjectInDirection(GameObject other, Vector2 dir, LayerMask layer)
    {
        RaycastHit2D rayCastHit = Physics2D.BoxCast(boxColl2d.bounds.center, boxColl2d.bounds.size,
             0, dir, 0.1f, layer);
        Debug.Log(rayCastHit.collider.gameObject);
        if (rayCastHit.collider == null) return false;
        if (rayCastHit.collider.gameObject == other)
        {
            return true;
        }
        return false;
    }
    private void flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
        horizontal *= -1;
    }
}
