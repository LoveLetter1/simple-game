using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Abandio.Debugger;
public class FireBallRuntime : MonoBehaviour
{
    public ParticleSystem particle;
    public float moveSpeed;
    public int damage;
    public Vector2 repelForce;
    public int filpX;
    public float destroyCounter;
    public float multipleFireBallRadius;
    public float multipleExplodeRadius;
    private void Update()
    {
        destroyCounter -= Time.deltaTime;
        transform.position += new Vector3(filpX, 0, 0) * (moveSpeed * Time.deltaTime);
        var coll = Physics2D.OverlapCircle(transform.position, 0.01f, 1 << 8 | 1 << 9);

        if (coll || destroyCounter <= 0f)
        {
            DebugDrawer.DrawCircle(transform.position,0.1f * multipleFireBallRadius,0.1f,filpX);
            //爆炸//造成伤害并击退
            Explode();
            //消失
            DestroyThis();
        }
        
    }
    #region Update Func

    private void Explode()
    {
        DebugDrawer.DrawCircle(transform.position, 0.1f * multipleFireBallRadius * multipleExplodeRadius,0.1f,filpX);
        var fireBallParticle = Instantiate(particle, transform.position, Unity.Mathematics.quaternion.Euler(new Vector3(0f,0f,0f )));

        var colls = Physics2D.OverlapCircleAll(transform.position, 0.1f * multipleFireBallRadius * multipleExplodeRadius, 1 << 8);
        foreach (var coll in colls)
        {
            var enemy = coll.GetComponent<Enemy>();
            enemy.BeRepeled(repelForce,filpX);
            enemy.BeAttacked(damage);
        }

    }

    private void DestroyThis()
    {
        Destroy(this.gameObject);
    }
    #endregion


    #region Public API
    public void Init(int filpX)
    {
        moveSpeed = 3f;
        damage = 2;
        repelForce = new Vector2(4f, 3f);
        this.filpX = filpX;
        destroyCounter = 5f;
        transform.localScale = Vector3.one * multipleFireBallRadius;
    }
    #endregion
}
