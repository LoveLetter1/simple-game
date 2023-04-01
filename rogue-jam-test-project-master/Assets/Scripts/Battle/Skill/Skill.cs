using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum SkillType
{
    Sword,Spear,Bow,All
}
public class Skill : ScriptableObject
{
    public string skillName;
    public SkillType skillType;
    public string skillRhythm; 
    public int mpUsage;
    public Sprite skillSprite;



    public virtual void ActiveSkill(GameObject obj)
    {
        return;
    }
}
