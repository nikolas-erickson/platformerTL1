
using UnityEngine;

public class Snail : Entity
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private GameObject snailShell;




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
        if (!isGroundInFront())
        {
            flip();
        }
        currentState.UpdateState(this);
    }


    void FixedUpdate()
    {
        currentState.FixedUpdateState(this);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player" && hitsObjectInDirection(collision.gameObject, Vector2.up, playerLayer))
        {
            kill();
        }
        currentState.OnCollisionEnter(this);
    }


    void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.gameObject.layer == LayerMask.NameToLayer("ground") &&
            hitsObjectInDirection(collision.gameObject, Vector2.right * horizontal, groundLayer))
        {
            flip();
        }
    }


    private bool hitsObjectInDirection(GameObject other, Vector2 dir, LayerMask layer)
    {
        RaycastHit2D rayCastHit = Physics2D.BoxCast(boxColl2d.bounds.center, boxColl2d.bounds.size,
             0, dir, 0.1f, layer);
        if (rayCastHit.collider == null) return false;
        if (rayCastHit.collider.gameObject == other)
        {
            return true;
        }
        return false;
    }


    private bool isGround()
    {
        RaycastHit2D rayCastHit = Physics2D.BoxCast(boxColl2d.bounds.center, boxColl2d.bounds.size,
             0, Vector2.down, 0.1f, groundLayer);
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
        horizontal *= -1;
    }

    public bool isVulnerableToJump()
    {
        return true;
    }

    public void kill()
    {
        //Instantiate(snailShell, transform.position, transform.rotation);
        //Destroy(gameObject);
        enterDeadState();
    }
}
