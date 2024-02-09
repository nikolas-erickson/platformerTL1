using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bodyParts : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        int _speed = 5;
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = new Vector2(UnityEngine.Random.Range(-1f, 1f) * _speed, UnityEngine.Random.Range(-1f, 1f) * _speed);


    }

    // Update is called once per frame
    void Update()
    {

    }
}
