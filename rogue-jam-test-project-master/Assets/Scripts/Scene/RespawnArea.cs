using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.name == "Player")
        {
            GameManager.Instance.SetRespawn(transform.position);
            Destroy(this.gameObject);
        }
    }
}
