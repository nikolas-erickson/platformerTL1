using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class whonpScript : MonoBehaviour
{
    enum whonpState
    {
        falling,
        idle,
        ascending,
        slam
    }

    private whonpState _myState;
    private float _timeLeftInState;
    private Vector3 _startPos;
    private Animator _animator;
    private Rigidbody2D _rigidBody;
    private BoxCollider2D _boxcollider;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _fallSpeed;
    [SerializeField] private float _riseSpeed;
    private Player _player;
    // Start is called before the first frame update
    void Start()
    {
        _myState = whonpState.idle;
        _timeLeftInState = Random.Range(1f, 3f);
        _startPos = transform.position;
        _animator = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _boxcollider = GetComponent<BoxCollider2D>();
        _player = Player.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        switch (_myState)
        {
            case whonpState.idle:
                _timeLeftInState -= Time.deltaTime;
                if (_timeLeftInState <= 0)
                {
                    _myState = whonpState.falling;
                    _animator.SetTrigger("triggerFall");
                    _rigidBody.velocity = Vector2.down * _fallSpeed;
                }
                break;
            case whonpState.falling:
                if (hitGround())
                {
                    _myState = whonpState.slam;
                    _rigidBody.velocity = Vector2.zero;
                    _animator.SetTrigger("triggerSlam");
                    _timeLeftInState = 0.7f;
                }
                break;
            case whonpState.slam:
                _timeLeftInState -= Time.deltaTime;
                if (_timeLeftInState <= 0)
                {
                    _myState = whonpState.ascending;
                    _rigidBody.velocity = Vector2.up * _riseSpeed;
                    _animator.SetTrigger("triggerIdle");
                }
                break;
            case whonpState.ascending:
                if (transform.position.y >= _startPos.y)
                {
                    _timeLeftInState = Random.Range(1f, 2f);
                    _rigidBody.velocity = Vector2.zero;
                    _myState = whonpState.idle;
                }
                break;
        }
    }

    bool hitGround()
    {
        RaycastHit2D rayCastHit = Physics2D.BoxCast(_boxcollider.bounds.center, _boxcollider.bounds.size,
             0, Vector2.down, 0.1f, _groundLayer);
        if (rayCastHit.collider == null) return false;
        return true;
    }

}
