using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using System;
using TMPro;

public class UIManager : UnitySingleton<UIManager>
{
    public Dictionary<string, PanelBase> panelDic = new Dictionary<string, PanelBase>();

    #region Public API

    public void RegisterPanel(string panelName, PanelBase panel)//注册Panel到UIManager
    {
        if (!panelDic.ContainsKey(panelName))
        {
            panelDic.Add(panelName, panel);
        }
    }
    public void CancelWidget(string name)//将Panel从UIManager中删除
    {
        if (panelDic.ContainsKey(name))
        {
            PanelBase panel = panelDic[name];
            panelDic.Remove(name);
            Destroy(panel.gameObject);
        }
    }
    public PanelBase GetPanelBase(string name)//获得PanelBase
    {
        if (panelDic.ContainsKey(name))
        {
            PanelBase panel = panelDic[name];
            return panel;
        }
        return null;
    }

    public UIBase GetWidget(string panelName, string widgetName)//获得UIBase
    {
        if (panelDic.ContainsKey(panelName))
        {
            PanelBase panel = panelDic[panelName];
            UIBase widget = panel.GetWidget(widgetName);
            return widget;
        }

        return null;
    }
    public void ShowUIPanel(string panelName)//显示UIPanel
    {
        if (panelDic.ContainsKey(panelName))
        {
            PanelBase panel = panelDic[panelName];
            panel.ShowMe();
        }
    }
    public void HideUIPanel(string panelName)//隐藏UIPanel
    {
        if (panelDic.ContainsKey(panelName))
        {
            PanelBase panel = panelDic[panelName];
            panel.HideMe();
        }
    }
    public void AddButtonDownListener(string panelName, string widgetName, UnityAction action)//添加按钮监听
    {
        UIBase widget = GetWidget(panelName, widgetName);
        if (widget != null)
        {
            Button button = widget.gameObject.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.AddListener(action);
            }
        }
    }
    public void AddGeneralListener(string panelName, string widgetName, EventTriggerType type, UnityAction<BaseEventData> action)//添加通用监听?
    {
        UIBase widget = GetWidget(panelName, widgetName);
        if (widget.GetComponent<EventTrigger>() == null)
        {
            widget.gameObject.AddComponent<EventTrigger>();
        }
        EventTrigger trigger = widget.GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = type;
        entry.callback.AddListener(action);
        trigger.triggers.Add(entry);
    }
    public void AddGeneralListener(string panelName, EventTriggerType type, UnityAction<BaseEventData> action)//添加通用监听?
    {
        PanelBase panel = GetPanelBase(panelName);
        if (panel.GetComponent<EventTrigger>() == null)
        {
            panel.gameObject.AddComponent<EventTrigger>();
        }
        EventTrigger trigger = panel.GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = type;
        entry.callback.AddListener(action);
        trigger.triggers.Add(entry);
    }
    public void ChangeText(string panelName, string widgetName, string text)//改变PanelBase下的UIBase的文本
    {
        UIBase widget = GetWidget(panelName, widgetName);
        TextMeshProUGUI uitext = widget.GetComponent<TextMeshProUGUI>();
        
        if (uitext != null)
        {
            uitext.text = text;
        }
    }
    public void ChangeButtonText(string panelName, string widgetName, string text)//改变PanelBase下的UIBase所包含的文本
    {
        UIBase widget = GetWidget(panelName, widgetName);
        TextMeshProUGUI uitext = widget.GetComponentInChildren<TextMeshProUGUI>();
        if (uitext != null)
        {
            uitext.text = text;
        }
    }
    public void SetImage(string panelName, string widgetName, Sprite image)//改变PanelBase下的UIBase所包含的图像
    {
        UIBase widget = GetWidget(panelName, widgetName);
        Image uiImage = widget.GetComponent<Image>();
        if (uiImage != null)
        {
            uiImage.sprite = image;
        }
    }
    public void AddInputFieldValueChangeListener(string panelName, string widgetName, UnityAction<string> action)//添加输入字段值更改侦听器?
    {
        UIBase widget = GetWidget(panelName, widgetName);
        InputField inputField = widget.gameObject.GetComponent<InputField>();
        if (inputField != null)
        {
            inputField.onValueChanged.AddListener(action);
        }
    }
    #endregion
}
