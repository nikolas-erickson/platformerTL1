using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    enum shellState
    {
        idle,
        moving,
        hit
    }
    enum MoveDir
    {
        left = -1,
        right = 1
    }
    private shellState _myState;
    private float _timeLeftInState;
    private Rigidbody2D _rigidBody;
    private BoxCollider2D _boxCollider;
    private Animator _animator;
    [SerializeField] private LayerMask playerLayer;
    private MoveDir _myDir;
    private float _moveSpeed;
    [SerializeField] private LayerMask groundLayer;
    // Start is called before the first frame update
    void Start()
    {
        _myState = shellState.idle;
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _moveSpeed = 7f;
    }

    // Update is called once per frame
    void Update()
    {
        switch (_myState)
        {
            case shellState.idle:
                break;
            case shellState.moving:
                break;
            case shellState.hit:

                break;
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("hit player");
            if (_myState == shellState.moving)
            {
                Debug.Log("while moving");
                if (hitsObjectInDirection(collision.gameObject, Vector2.up, playerLayer))
                {
                    Debug.Log("1");
                    _rigidBody.velocity = Vector2.zero;
                    _animator.SetTrigger("triggerHit");
                    _myState = shellState.idle;
                }
                else
                {
                    Debug.Log("2");
                    Player.Instance.getHurt();
                }
            }
            else if (_myState == shellState.idle)
            {
                Debug.Log("while idle");
                if (hitsObjectInDirection(collision.gameObject, Vector2.up, playerLayer))
                {
                    Debug.Log("1");
                }
                else if (hitsObjectInDirection(collision.gameObject, Vector2.left, playerLayer))
                {
                    Debug.Log("2");
                    _rigidBody.velocity = Vector2.right * _moveSpeed;
                    _myDir = MoveDir.right;
                    _animator.SetTrigger("triggerPush");
                    _myState = shellState.moving;
                }
                else if (hitsObjectInDirection(collision.gameObject, Vector2.right, playerLayer))
                {
                    Debug.Log("3");
                    _rigidBody.velocity = Vector2.right * _moveSpeed;
                    _myDir = MoveDir.right;
                    _animator.SetTrigger("triggerPush");
                    _myState = shellState.moving;
                }
                else
                {
                    Debug.Log("4");
                }
            }
            else
            {
                Debug.Log("while blank??");
            }
        }
        else if (collision.gameObject.tag == "trap")
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "enemy")
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.gameObject.layer == LayerMask.NameToLayer("ground") &&
            hitsObjectInDirection(collision.gameObject, Vector2.right * (int)_myDir, groundLayer))
        {
            _myDir = (Shell.MoveDir)(-(int)_myDir);
            Debug.Log("shell moving " + _myDir);
            _rigidBody.velocity = new Vector2(-_rigidBody.velocity.x, _rigidBody.velocity.y);
        }
    }

    private bool hitsObjectInDirection(GameObject other, Vector2 dir, LayerMask layer)
    {
        RaycastHit2D rayCastHit = Physics2D.BoxCast(_boxCollider.bounds.center, _boxCollider.bounds.size,
             0, dir, 0.1f, layer);
        if (rayCastHit.collider == null) return false;
        if (rayCastHit.collider.gameObject == other)
        {
            return true;
        }
        return false;
    }
}
