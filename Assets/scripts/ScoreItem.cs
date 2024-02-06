using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreItem : MonoBehaviour
{
    [SerializeField] protected int value;
    [SerializeField] private GameObject collectedItem;

    protected void claimAndDestroy()
    {
        GameController.Instance.addToPoints(value);
        removeThis();
    }

    private void removeThis()
    {

        Instantiate(collectedItem, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
