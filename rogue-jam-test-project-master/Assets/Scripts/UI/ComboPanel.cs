using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ComboPanel : PanelBase
{
    public int comboCount;
    protected override void Awake()
    {
        base.Awake();
    }



    public void Show()
    {
        GetComponent<CanvasGroup>().alpha = 1;
        
    }
    public void Hide()
    {
        GetComponent<CanvasGroup>().alpha = 0;

    }
    public void RefreshCombo()
    {
        Show();//TODO:临时，后续改为Show一次
        comboCount++;
        UIManager.Instance.ChangeText(this.name, "ComboCounter_U", comboCount.ToString());
    }
    public void BreakCombo()
    {
        comboCount = 0;
        Hide();
    }
    public void Reset()
    {
        comboCount = 0;
    }

}
