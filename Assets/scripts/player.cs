//using System.Collections;
//using System.Collections.Generic;
//using UnityEditor.Animations;
//using UnityEditor.Tilemaps;
using UnityEngine;

public class player : Entity
{
    // Variables
    private bool isFacingRight = true;
    [SerializeField] private LayerMask groundLayer;
    public Transform headCheck;
    public LayerMask brickLayer;
    public LayerMask enemyLayer;
    private AudioSource audioSource;
    [SerializeField] private AudioClip jumpSound;
    private bool tempInvulnerable;
    private float timeInvulnerable;
    void Start()
    {
        tempInvulnerable = false;
        initializeComponents();
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) Debug.LogError("audioSource reference is null.");
        enterIdleState();
    }

    // Update is called once per frame
    void Update()
    {
        //handle temporary invulnerability
        if (tempInvulnerable)
        {
            timeInvulnerable -= Time.deltaTime;
            if (timeInvulnerable <= 0)
            {
                tempInvulnerable = false;
            }
        }


        //store input in horizontal variable
        horizontal = Input.GetAxis("Horizontal");

        //handle input related to jumping
        if (Input.GetButtonDown("Jump") && isGround() && currentState != jumpState)
        {
            audioSource.PlayOneShot(jumpSound, 1);
            //enterJumpState();
            enterJumpState();
        }



        flip();
        currentState.UpdateState(this);
    }

    private void FixedUpdate()
    {
        currentState.FixedUpdateState(this);
    }

    private bool isGround()
    {
        RaycastHit2D rayCastHit = Physics2D.BoxCast(boxColl2d.bounds.center, boxColl2d.bounds.size,
             0, Vector2.down, 0.1f, groundLayer);
        return rayCastHit.collider != null;
    }

    private void flip()
    {
        if ((isFacingRight && horizontal < 0f) || (!isFacingRight && horizontal > 0f))
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Brick" && isCollidingWithBrick())
        {
            //play sound for breaking brick
            Destroy(collision.gameObject);
        }
        currentState.OnCollisionEnter(this);

        if (collision.gameObject.tag == "enemy" && hitsObjectInDirection(collision.gameObject, Vector2.down))
        {
            enterJumpState();
            //collision.gameObject.kill();
        }
        else if (collision.gameObject.tag == "enemy")
        {
            //bad touch
            enterDeadState();
        }
    }
    private bool isCollidingWithBrick()
    {
        return Physics2D.OverlapCircle(headCheck.position, 0.2f, brickLayer);
    }



    private bool hitsObjectInDirection(GameObject other, Vector2 dir)
    {
        RaycastHit2D rayCastHit = Physics2D.BoxCast(boxColl2d.bounds.center, boxColl2d.bounds.size,
             0, dir, 0.1f, enemyLayer);
        if (rayCastHit.collider == null) return false;
        if (rayCastHit.collider.gameObject == other)
        {
            return true;
        }
        return false;
    }
}
