using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectedItem : MonoBehaviour
{
    private float _lifetime;
    // Start is called before the first frame update
    void Start()
    {
        _lifetime = 0.35f;
    }

    // Update is called once per frame
    void Update()
    {
        _lifetime -= Time.deltaTime;
        if (_lifetime < 0)
        {
            Destroy(gameObject);
        }
    }
}
