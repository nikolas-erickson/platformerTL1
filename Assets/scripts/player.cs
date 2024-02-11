//using System.Collections;
//using System.Collections.Generic;
//using UnityEditor.Animations;
//using UnityEditor.Tilemaps;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// Player entity is created for use as a singleton
/// </summary>
public class Player : Entity
{
    public static Player Instance { get; private set; } // use as singleton

    // Variables
    private bool isFacingRight = true;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask interactableLayer;
    public Transform headCheck;
    public LayerMask brickLayer;
    public LayerMask enemyLayer;
    private AudioSource audioSource;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip winSound;
    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private AudioClip themeSound;
    [SerializeField] private AudioClip walkSound;
    [SerializeField] private GameObject bodyParts;
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
            if (transform.position.y < -8)
            {
                getHurt();
            }
            horizontal = Input.GetAxis("Horizontal");

            //handle input related to jumping
            if (Input.GetButtonDown("Jump") && (isOnTop(groundLayer) || isOnTop(interactableLayer)) && currentState != jumpState)
            {
                audioSource.PlayOneShot(jumpSound, 1);
                //enterJumpState();
                enterJumpState();
            }
            flip();
            currentState.UpdateState(this);
        }
    }

    private void FixedUpdate()
    {
        if (_playing)
        {
            currentState.FixedUpdateState(this);
        }
    }

    private bool isOnTop(LayerMask layer)
    {
        RaycastHit2D rayCastHit = Physics2D.BoxCast(boxColl2d.bounds.center, boxColl2d.bounds.size,
             0, Vector2.down, 0.2f, layer);
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
            Debug.Log("hit enemy");
            if (hitsObjectInDirection(collision.gameObject, Vector2.down, enemyLayer))
            {
                enterJumpState();
                //collision.gameObject.kill();
            }
            else
            {
                getHurt();
            }

        }
        else if (collision.gameObject.tag == "trap")
        {
            getHurt();

        }
        else if (collision.gameObject.tag == "goal")
        {
            rigidBody.bodyType = RigidbodyType2D.Static;
            audioSource.PlayOneShot(winSound, 1);
            _playing = false;
            GameController.Instance.storeLevelComplete();
            CameraScript.Instance.finalZoom(transform.position);
            //win animation?
            SceneManager.LoadScene("levelSelect");
        }
        else if (collision.gameObject.tag == "trampoline")
        {
            if (hitsObjectInDirection(collision.gameObject, Vector2.down, interactableLayer))
            {
                enterJumpState();
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, 25);
            }
        }
        currentState.OnCollisionEnter(this);
    }
    private bool isCollidingWithBrick()
    {
        return Physics2D.OverlapCircle(headCheck.position, 0.2f, brickLayer);
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


    public void getHurt()
    {
        CameraScript.Instance.finalZoom(transform.position);
        audioSource.PlayOneShot(hurtSound, 1);
        _playing = false;
        //create bloody body parts when dead
        for (int i = 0; i < 7; i++)
        {
            Instantiate(bodyParts, transform.position, transform.rotation);
        }
        //set timer for dramatic camera zoom
        GameController.Instance.startTmrReturnToLvlSelect();
        //remove player instance
        Destroy(gameObject);
        //enterDeadState();
    }
}
