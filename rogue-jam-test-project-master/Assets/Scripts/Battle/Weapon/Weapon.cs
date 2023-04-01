using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Abandio.Debugger;
using UnityEngine.Serialization;

public class Weapon : ScriptableObject
{
    public string weaponName;
    public Sprite weaponSprite;
    [Header("攻击判定属性")]
    public bool isHitWeapon;
    public bool isCreateArrow;//是否生成箭
    public Vector3 hitPoint;
    public float hitRadius;
    [Header("攻击属性")] 
    public int damage;

    public Vector2 repelForce;
    public virtual void CheckHit(Vector3 charPos, int filpX)
    {
        if (!isHitWeapon)
            return;
        DebugDrawer.DrawCircle(charPos + hitPoint,hitRadius,0.1f,filpX);
        Collider2D[] colls = Physics2D.OverlapCircleAll(charPos + hitPoint * filpX, hitRadius, 1 << 8);
        if (colls.Length != 0)
        {
            Enemy[] enemies = new Enemy[colls.Length];
            for (int idx = 0; idx < colls.Length; idx++)
            {
                enemies[idx] = colls[idx].GetComponent<Enemy>();
                enemies[idx].BeAttacked(damage);
                enemies[idx].BeRepeled(repelForce,filpX);
            }

            CamManager.Instance.Shake(1.5f, 1f, 0.1f);
            Debug.Log("Hit" + enemies.Length);
        }
        else
        {
            
        }
    }

    public virtual GameObject GetArrowObj()
    {
        return null;
    }

    public virtual void SetSkill(Skill skill)
    {
        return;
    }
}
