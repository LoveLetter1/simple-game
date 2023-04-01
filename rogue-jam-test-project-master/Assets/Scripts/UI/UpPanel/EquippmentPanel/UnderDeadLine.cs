using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderDeadLine : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D coll)
    {
        switch (coll.gameObject.name)
        {
            case "Player":
                coll.transform.position = GameManager.Instance.respawnV3;
                break;
            default:
                Destroy(coll.gameObject);
                break;
        }
    }
}
