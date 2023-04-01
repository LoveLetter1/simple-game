using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Abandio.Debugger;

[CreateAssetMenu(fileName = "ShockWave",menuName = "Battle/Skill/ShockWave")]
public class ShockWave : Skill
{
    public ParticleSystem particle;
    public Vector2 pushForce;
    public Vector2 influentSize;

    public Vector2 influentCenter { get{return influentSize / 2;} }


    public override void ActiveSkill(GameObject obj)
    {
        var localScale = obj.transform.localScale;
        var position = obj.transform.position;
        Debug.Log("冲击波启用");

        var ShockWaveParticle = Instantiate(particle, position, Unity.Mathematics.quaternion.Euler(new Vector3(0f,90f * localScale.x,0f )));

        //TODO: 消除敌方飞行道具、物品
        //TODO: 给前方单位施加玩家->单位的力，值为pushForce
        var colls = Physics2D.OverlapBoxAll(Vector2.positiveInfinity + influentCenter * localScale.x, influentSize, default,1 << 8);
        for (int idx = 0; idx < colls.Length; idx++)
        {
            colls[idx].GetComponent<Rigidbody2D>().AddForce(pushForce,ForceMode2D.Impulse);
            //TODO: 需要×方向
        }
        
        //TODO: 消耗玩家的Mp
    }
}
