using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trampolineScript : MonoBehaviour
{
    private float bounceTime;
    private bool isBouncing;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        //anim.enabled = false;
        isBouncing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isBouncing)
        {
            bounceTime -= Time.deltaTime;
            if (bounceTime <= 0)
            {
                isBouncing = false;
                anim.SetTrigger("triggerIdle");
            }
        }

    }

    public void triggerBounce()
    {
        if (!isBouncing)
        {
            anim.SetTrigger("triggerBounce");
            isBouncing = true;
            bounceTime = 0.35f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            triggerBounce();
        }
    }
}
