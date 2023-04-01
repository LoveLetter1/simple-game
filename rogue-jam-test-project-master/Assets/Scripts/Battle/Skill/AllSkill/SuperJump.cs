using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SuperJump",menuName = "Battle/Skill/SuperJump")]

public class SuperJump : Skill
{
   public float jumpForce;

   public override void ActiveSkill(GameObject obj)
   {
      obj.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
      
      obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(0,jumpForce),ForceMode2D.Impulse);
   }
}
