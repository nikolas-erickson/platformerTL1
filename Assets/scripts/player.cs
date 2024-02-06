//using System.Collections;
//using System.Collections.Generic;
//using UnityEditor.Animations;
//using UnityEditor.Tilemaps;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Entity
{
    public static Player Instance { get; private set; }

    // Variables
    private bool isFacingRight = true;
    [SerializeField] private LayerMask groundLayer;
    public Transform headCheck;
    public LayerMask brickLayer;
    public LayerMask enemyLayer;
    private AudioSource audioSource;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip winSound;
    private bool tempInvulnerable;
    private float timeInvulnerable;
    [SerializeField] GameController logicController;
    private bool _playing;

    // needed for singleton
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // dont allow 2 Player.Instance
        }
    }



    void Start()
    {
        _playing = true;
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
        if (_playing)
        {
            horizontal = Input.GetAxis("Horizontal");

            //handle input related to jumping
            if (Input.GetButtonDown("Jump") && isGround() && currentState != jumpState)
            {
                audioSource.PlayOneShot(jumpSound, 1);
                //enterJumpState();
                enterJumpState();
            }
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
        else if (collision.gameObject.tag == "enemy")
        {
            Debug.Log("checking collision " + collision.gameObject);
            if (hitsObjectInDirection(collision.gameObject, Vector2.down))
            {
                enterJumpState();
                //collision.gameObject.kill();
            }
            else
            {
                //bad touch
                enterDeadState();
            }

        }
        else if (collision.gameObject.tag == "goal")
        {
            rigidBody.bodyType = RigidbodyType2D.Static;
            audioSource.PlayOneShot(winSound, 1);
            _playing = false;
            //CameraScript.Instance.finalZoom(transform.position);
        }
        currentState.OnCollisionEnter(this);
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

    /*  handled in item script
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("points"))
        {
            if (collision.gameObject.tag == "melon")
            {
                logicController.addToPoints(10);
                Destroy(collision.gameObject);
            }
            else if (collision.gameObject.tag == "pineapple")
            {
                logicController.addToPoints(20);
                Destroy(collision.gameObject);
            }
        }
    }
    */
}
