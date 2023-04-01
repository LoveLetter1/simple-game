using System;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

public enum EnemyStatus
{
    Idle,Run,FindPlayer,BeAttacked,Death
}
public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    [Header("路径属性")]
    public float patrolRadius;
    public bool canPatrol;
    public Vector3 leftBound;
    public Vector3 rightBound;
    public float idleCounterTime;
    public float idleTime;
    public Vector3 direction;
    
    [Header("属性")]
    public int hp;
    public int damage;
    public float moveSpeed;
    public EnemyStatus enemyStatus;
    public float multipleSlowDown;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        multipleSlowDown = GameManager.Instance.multipleSlowDown;
    }



    private void FixedUpdate()
    {
        switch (enemyStatus)
        {
            case EnemyStatus.Idle:
                if (canPatrol)
                {
                    idleCounterTime -= Time.deltaTime * multipleSlowDown;
                    if (idleCounterTime <= 0f)
                    {
                        direction *= -1;
                        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                        enemyStatus = EnemyStatus.Run;
                        anim.Play("Run");
                    }
                }

                break;
            case EnemyStatus.Run:
                
                transform.position += direction * (moveSpeed * Time.deltaTime * multipleSlowDown);
                if (Vector3.Distance(leftBound, transform.position) <= 0.1f || Vector3.Distance(rightBound, transform.position) <= 0.1f)
                {
                    idleCounterTime = idleTime;
                    enemyStatus = EnemyStatus.Idle;
                    anim.Play("Idle");
                }

                
                break;
            case EnemyStatus.BeAttacked:

                break;
            case EnemyStatus.FindPlayer:

                break;
            case EnemyStatus.Death:

                break;
            default: break;
        }
        
    }



    public void CreateInit()
    {
        RaycastHit2D hit1, hit2;
        var position = transform.position;
        hit1 = Physics2D.Raycast(position, Vector2.left, 10f, 1 << 9);
        hit2 = Physics2D.Raycast(position, Vector2.left, 10f, 1 << 9);
        if(hit1.collider && hit2.collider)
        {
            if (Vector3.Distance(hit1.point, hit2.point) <= 0.5f)
            {
                Debug.Log("左右距离过小,设置为站立不动");
                canPatrol = false;
                return;
            }
             
            if(Vector3.Distance(transform.position,hit1.point) < Vector3.Distance(transform.position, hit2.point))
            {
                leftBound = hit1.point;
                rightBound = hit1.point + new Vector2(patrolRadius * 2, 0);

            }
            else
            {
                rightBound = hit2.point;
                leftBound = hit2.point - new Vector2(patrolRadius * 2, 0);
            }
        }
        else
        {
            RaycastHit2D hit = !hit1.collider ? !hit2.collider ? new RaycastHit2D() : hit2 : hit1;
            if (hit.collider)
            {
                if(hit.point.x < transform.position.x)
                {
                    leftBound = hit.point;
                    rightBound = hit.point + new Vector2(patrolRadius * 2, 0);
                }
                else
                {
                    rightBound = hit.point;
                    leftBound = hit.point - new Vector2(patrolRadius * 2, 0);
                }
            }
            else
            {
                var position1 = transform.position;
                leftBound = position1 + new Vector3(-patrolRadius, 0, 0);
                rightBound = position1 + new Vector3(patrolRadius, 0, 0);
                
            }
        }
        canPatrol = true;
        direction = Random.Range(0, 1) == 0 ? Vector3.left : Vector3.right;
        if (direction == Vector3.left)
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        anim.Play("Run");
        
        enemyStatus = canPatrol ? EnemyStatus.Run : EnemyStatus.Idle;
        idleCounterTime = idleTime;
        multipleSlowDown = 1f;
    }
    public void BeAttacked(int attackerDamage)
    {
        hp -= attackerDamage;
        if (hp <= 0)
        {
            Death();

        }
    }

    public void BeRepeled(Vector2 repelForce,int filpX)
    {
        enemyStatus = EnemyStatus.BeAttacked;
        rb.velocity = new Vector2(0f, repelForce.y == 0f ? rb.velocity.y : 0f);
        rb.AddForce(new Vector2(repelForce.x * filpX,repelForce.y),ForceMode2D.Impulse);
        anim.Play("BeAttacked");
    }
    private void Death()
    {
        Destroy(gameObject);
    }

    public void ResetAnim()
    {
        anim.Play("Idle");
        enemyStatus = EnemyStatus.Idle;
    }
}
