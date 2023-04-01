using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dash",menuName = "Battle/Skill/Dash")]
public class Dash : Skill
{
    public Vector2 dashForce;
    
    public override void ActiveSkill(GameObject obj)
    {
        Debug.Log("使用冲刺技能");
        obj.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        obj.GetComponent<Rigidbody2D>().AddForce(dashForce * obj.transform.localScale.x,ForceMode2D.Impulse);


    }
}
