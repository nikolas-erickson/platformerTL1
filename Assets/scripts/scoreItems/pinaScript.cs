using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pinaScript : ScoreItem
{
    // Start is called before the first frame update
    void Start()
    {
        //value = 10;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            if (collision.gameObject.tag == "Player")
            {
                claimAndDestroy();
            }
        }
    }
}
