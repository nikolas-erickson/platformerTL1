using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    [SerializeField] private Transform player;

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x > 0)
        {
            transform.position = new Vector3(player.transform.position.x, 0, -10);
        }
    }
}
