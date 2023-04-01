using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TapLevelPanel : PanelBase
{
    [SerializeField]
    private TextMeshProUGUI missTMP;
    [SerializeField]
    private TextMeshProUGUI goodTMP;
    [SerializeField]
    private TextMeshProUGUI perfectTMP;

    private TextMeshProUGUI isShowingTMP;

    protected override void Awake()
    {
        base.Awake();
    }


    public void ShowTapLevelText(TapLevel tapLevel)
    {
        switch (tapLevel)
        {
            case TapLevel.Miss:
                Show(missTMP);
                break;
            case TapLevel.Good:
                Show(goodTMP);

                break;
            case TapLevel.Perfect:
                Show(perfectTMP);

                break;
            default:
                break;
        }
    }
    public void HideTapLevelText()
    {
        if (isShowingTMP)
        {
            GetWidget(isShowingTMP.name).HideMe();

            isShowingTMP = null;
        }

    }
    private void Show(TextMeshProUGUI TMP)
    {
        if (isShowingTMP)
        {
            if(isShowingTMP != TMP)
            {
                GetWidget(isShowingTMP.name).HideMe();
                isShowingTMP = TMP;
                GetWidget(isShowingTMP.name).ShowMe();

            }
        }
        else
        {
            isShowingTMP = TMP;
            GetWidget(isShowingTMP.name).ShowMe();

        }

    }


}
