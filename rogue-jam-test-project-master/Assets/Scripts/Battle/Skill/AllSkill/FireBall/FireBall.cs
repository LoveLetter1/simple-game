using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "FireBall",menuName = "Battle/Skill/FireBall")]

public class FireBall : Skill
{
    public GameObject fireBallPrefab;

    
    public override void ActiveSkill(GameObject obj)
    {
        Debug.Log("使用火球技能");
        var localScale = obj.transform.localScale;
        var v3 = new Vector3(0, 0, -90 * localScale.x);
        Quaternion qua = Quaternion.Euler(v3);
        GameObject fireBall = Instantiate(fireBallPrefab, obj.transform.position, qua);
        fireBall.layer = 10;
        fireBall.GetComponent<FireBallRuntime>().Init((int)localScale.x);

    }
}
