using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EndGamePoint : MonoBehaviour
{
    public bool isTouched;
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.name == "Player" && !isTouched)
        {
            isTouched = true;
            UIManager.Instance.ShowUIPanel("GameOverPanel");
        }
    }
}
