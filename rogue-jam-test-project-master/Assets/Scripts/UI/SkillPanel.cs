using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillPanel : PanelBase
{
    private TextMeshProUGUI[] tmps;
    private Image[] images;
    public int tmpIdx;
    protected override void Awake()
    {
        base.Awake();
        tmps = GetComponentsInChildren<TextMeshProUGUI>();
        images = GetComponentsInChildren<Image>();
    }

    public void SetSkillRhythm(Skill skill)
    {
        tmps[tmpIdx].text = skill.skillRhythm;
        images[tmpIdx].enabled = true;
        images[tmpIdx++].sprite = skill.skillSprite;
    }
}
