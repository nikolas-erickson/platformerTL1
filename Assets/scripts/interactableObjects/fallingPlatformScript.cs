using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallingPlatformScript : MonoBehaviour
{
    private bool _touched;
    private bool _falling;
    private bool _reset;
    private float _timeLeft;
    private Vector3 _position;

    private Rigidbody2D _rigidBody;
    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _touched = false;
        _falling = false;
        _reset = false;
        _position = transform.position;
        _timeLeft = 2f;
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_touched)
        {
            _timeLeft -= Time.deltaTime;
            if (_timeLeft <= 0)
            {
                _falling = true;
                _touched = false;
                _rigidBody.velocity = new Vector2(0, -4);
                _timeLeft = 2f;
            }
        }
        else if (_falling)
        {
            _timeLeft -= Time.deltaTime;
            //_rigidBody.rotation = new
            if (_timeLeft <= 0)
            {
                _falling = false;
                _reset = true;
                _rigidBody.velocity = Vector2.zero;
                _timeLeft = 5f;
                transform.position = new Vector3(0, 30, 0);
            }
        }
        else if (_reset)
        {
            _timeLeft -= Time.deltaTime;
            if (_timeLeft <= 0)
            {
                _reset = false;
                transform.position = _position;
                _animator.SetTrigger("triggerReset");
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!_touched && !_falling)
            {
                _touched = true;
                _timeLeft = 1f;
                _animator.SetTrigger("triggerOff");
            }
        }
    }
}
