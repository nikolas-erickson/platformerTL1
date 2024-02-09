using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireTrapScript : MonoBehaviour
{
    private bool _on;
    private bool _firing;
    private float _timeLeft;
    private Vector3 _deathRegionOffset;
    private GameObject _myDeathRegion;

    private Animator _animator;

    [SerializeField] GameObject deathRegion;

    void Start()
    {
        _deathRegionOffset = new Vector3(.06f, .3f, 0);
        _on = false;
        _firing = false;
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_on)
        {
            _timeLeft -= Time.deltaTime;
            if (_timeLeft <= 0)
            {
                //start firing
                _firing = true;
                _on = false;
                _timeLeft = 2f;
                _animator.SetTrigger("triggerFire");
                _myDeathRegion = Instantiate(deathRegion, transform.position + _deathRegionOffset, Quaternion.identity);
            }
        }
        else if (_firing)
        {
            _timeLeft -= Time.deltaTime;
            if (_timeLeft <= 0)
            {
                //reset
                _firing = false;
                _animator.SetTrigger("triggerIdle");
                Destroy(_myDeathRegion);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!_on && !_firing)
            {
                _on = true;
                _timeLeft = 1f;
                _animator.SetTrigger("triggerOn");
            }
        }
    }
}
