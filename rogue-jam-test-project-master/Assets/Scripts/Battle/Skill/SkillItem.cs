using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillItem : MonoBehaviour
{
    public Skill skill;
    public SpriteRenderer sr;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        sr.sprite = skill.skillSprite;
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        Debug.Log(coll.name);
        if(!coll)
            return;
        if (coll.gameObject.name == "Player")
        {
            coll.GetComponent<EquipmentController>().AddSkill(skill);
            GameManager.Instance.DicAdd<String,Skill>(GameManager.Instance.DicSkill,skill.skillRhythm,skill);
            UIManager.Instance.GetPanelBase("SkillPanel").GetComponent<SkillPanel>().SetSkillRhythm(skill);
            Destroy(this.gameObject);
        }
    }
}
