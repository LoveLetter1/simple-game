using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PropmptPanel : PanelBase
{
    public TextMeshProUGUI tmp;

    protected override void Awake()
    {
        base.Awake();
        tmp = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetText(string str)
    {
        tmp.text = str;
    }

    public void ResetText()
    {
        tmp.text = null;
    }
}
