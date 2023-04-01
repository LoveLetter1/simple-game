using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentController : MonoBehaviour
{
    public Animator charAnim;
    [Header("人物武器特效三合一")]
    public bool isCombined;

    [Header("武器")]
    public Weapon[] weapon;

    public List<Skill> skills;

    public bool isAttacking;
    public bool hadHit;
    public int weaponIdx = -1;

    private void Start()
    {
        var gameManager = GameManager.Instance;
        for (int idx = 0; idx < skills.Count; idx++)
        {
            var skill = skills[idx];
            if (skill == null || gameManager.DicSkill.ContainsKey(skill.skillRhythm))
                continue;

            gameManager.DicAdd<string,Skill>(gameManager.DicSkill,skill.skillRhythm,skill);
        }
        var equipmentPanel = UIManager.Instance.GetPanelBase("EquipmentPanel").GetComponent<EquipmentPanel>();
        equipmentPanel.SetWeaponPanel(KeyCode.J,weapon[0]);
        equipmentPanel.SetWeaponPanel(KeyCode.K,weapon[1]);
    }

    public void SetWeapon(int idx, Weapon weapon)
    {
        this.weapon[idx] = weapon;
    }
    public void Attack(KeyCode keyCode)
    {
        if(isAttacking )
            return;
        isAttacking = true;
        switch (keyCode)
        {
            case KeyCode.J:
                charAnim.Play("ATK1");
                weaponIdx = 0;
                break;
            case KeyCode.K:
                charAnim.Play("ATK2");
                weaponIdx = 1;

                break;
            default:
                break;
        }
        
        //TODO: 人物武器特效分离
        //charAnim.Play("Attack1");
        //if (!isCombined)
        //{
        //weaponAnim.Play("");
        //effectAnim.Play("");
        //}

    }

    public void CheckHit()
    {
        if (hadHit)
            return;
        weapon[weaponIdx].CheckHit(transform.position,(int)transform.localScale.x);
        
    }
    public virtual void EndAttack()
    {
        var playerController = GetComponent<PlayerController>();
        if (playerController.isJumping)
        {
            playerController.playerStatus = PlayerStatus.Jump;
            charAnim.Play("Jump");
        }
        else
        {
            playerController.playerStatus = PlayerStatus.Idle;
            charAnim.Play("Idle");

        }

        isAttacking = false;
        hadHit = false;
        weaponIdx = -1;
    }

    public void AddSkill(Skill skill)
    {
        if(skills.Contains(skill))
            return;
        skills.Add(skill);
    }

}
