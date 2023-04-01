using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class LinkedButton : MonoBehaviour
{
    public GameObject gate;
    private Animator anim;
    private bool isPressed;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }


    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (!coll || isPressed || !gate)
            return;

        
        if (coll.gameObject.layer == 10 )
        {
            anim.Play("Pressed");
            Destroy(gate.gameObject);
        }
    }
}
