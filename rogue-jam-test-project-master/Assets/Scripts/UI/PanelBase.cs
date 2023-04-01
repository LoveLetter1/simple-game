using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PanelBase : MonoBehaviour
{
    Dictionary<string, UIBase> uiBaseDic = new Dictionary<string, UIBase>();
    CanvasGroup view;
    protected virtual void Awake()
    {
        //添加CanvasGroup组件
        if (gameObject.GetComponent<CanvasGroup>() == null)
            view = gameObject.AddComponent<CanvasGroup>();
        else
            view = gameObject.GetComponent<CanvasGroup>();
        //将此Panel注册到UIManager中
        UIManager.Instance.RegisterPanel(gameObject.name, this);
        //将子类中所有后缀为_U的UI添加UIBase组件
        Transform[] allChildren;
        allChildren = transform.GetComponentsInChildren<Transform>();
        foreach (var child in allChildren)
        {
            if (child.name.EndsWith("_U") && !child.GetComponent<UIBase>())
            {
                child.gameObject.AddComponent<UIBase>();
            }
        }
    }
    public void RegisterWidget(string name, UIBase widget)//将UIBase添加到PanelBase中
    {
        if (!uiBaseDic.ContainsKey(name))
        {
            uiBaseDic.Add(name, widget);
        }
    }
    public void CancelWidget(string name)//将UIBase从PanelBase中删除
    {
        if (uiBaseDic.ContainsKey(name))
        {
            uiBaseDic.Remove(name);
        }
    }
    void OnDestroy()
    {
        uiBaseDic.Clear();
        uiBaseDic = null;
    }
    public UIBase GetWidget(string name)//获得UIBase
    {
        if (uiBaseDic.ContainsKey(name))
        {
            UIBase widget = uiBaseDic[name];
            return widget;
        }
        return null;
    }
    public virtual void ShowMe()
    {
        view.alpha = 1;

    }
    public virtual void HideMe()
    {
        view.alpha = 0;

    }
    public void AddButtonDownListener(string widgetName, UnityAction action)//为UIBase添加按钮监听
    {
        UIBase widget = GetWidget(widgetName);
        Button button = widget.gameObject.GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(action);
        }
    }
}
