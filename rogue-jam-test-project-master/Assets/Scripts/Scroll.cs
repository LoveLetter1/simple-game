using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    public string str;
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.name == "Player")
        {
            UIManager.Instance.GetPanelBase("PromptPanel").GetComponent<PropmptPanel>().SetText(str);
            
        }
    }
    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.name == "Player")
        {
            UIManager.Instance.GetPanelBase("PromptPanel").GetComponent<PropmptPanel>().ResetText();
        }
    }
}
