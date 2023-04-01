using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBase : MonoBehaviour
{
    CanvasGroup view;
    void Awake()
    {
        PanelBase panel = GetComponentInParent<PanelBase>();
        panel.RegisterWidget(gameObject.name, this);
        view = GetComponent<CanvasGroup>();
    }

    public void ShowMe()
    {
        view.alpha = 1;
    }
    public void HideMe()
    {
        view.alpha = 0;
    }
}
