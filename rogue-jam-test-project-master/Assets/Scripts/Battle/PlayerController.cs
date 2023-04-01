using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;
using UnityEngine.SocialPlatforms;

public enum PlayerStatus//状态机
{
    Idle,Run,Jump,Attack,Death
}
public class PlayerController : MonoBehaviour
{
    public PlayerStatus playerStatus;
    public EquipmentController equipmentController;
    public Animator anim;
    public Rigidbody2D rb;
    public float moveSpeed = 4;
    public float jumpSpeed = 5;
    public float moveDir;
    public float multipleSlowDown = 1;

    public RaycastHit2D leftHit;
    public RaycastHit2D midHit;
    public RaycastHit2D rightHit;

    private float unlockJumpResTime = 0.25f;
    public float unlockJumpResTimeCounter;
    //属性
    public int hp;
    public int maxMp;
    public int mp;
    public float mpRecoverTimeCounter;
    public float mpRecoverTime;
    
    
    //状态
    public bool isJumping;
    public bool isOnGround;

    private void Awake()
    {
        // ActiveSkillEv += GetComponent<EquipmentController>()
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        equipmentController = GetComponent<EquipmentController>();
        multipleSlowDown = 1f;
        GameManager.Instance.SetTimeScaleEv += SetPlayerTimeScale;
        GameManager.Instance.ResetTimeScaleEv += ResetPlayerTimeScale;
        
    }
    
    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.F1))
        {
            DebugReset();
        }
        moveDir = Input.GetAxisRaw("Horizontal");
        
        leftHit = Physics2D.Raycast(transform.position + new Vector3(-0.35f, -0.6f, 0), Vector2.down, 0.05f,1<<9);   
        midHit = Physics2D.Raycast(transform.position + new Vector3(0, -0.6f, 0), Vector2.down, 0.05f,1<<9);   
        rightHit = Physics2D.Raycast(transform.position + new Vector3(0.35f, -0.6f, 0), Vector2.down, 0.05f,1<<9);   
        isOnGround = (leftHit || midHit || rightHit) ? true : false;
    

        switch ( playerStatus)
        {
            case PlayerStatus.Attack:
                if (isJumping && equipmentController.isAttacking && 
                    anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Idle" || 
                    anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Jump")
                {
                    Debug.Log(1);
                    DebugReset();
                }
                break;
            case PlayerStatus.Death:
                    
                break;
            case PlayerStatus.Idle:
                if (moveDir != 0)
                {
                    playerStatus = PlayerStatus.Run;
                    anim.Play("Run");
                    break;
                }

                Jump();
                
                Attack(KeyCode.J);
                Attack(KeyCode.K);
                break;
            case PlayerStatus.Jump:
                unlockJumpResTimeCounter -= Time.deltaTime;
                if (isOnGround)
                {
                    QuitJumpStatus();
                }


                Attack(KeyCode.J);
                Attack(KeyCode.K);
                Move();
                break;
            case PlayerStatus.Run:
                
                if (moveDir == 0)
                {
                    playerStatus = PlayerStatus.Idle;
                    rb.velocity = Vector2.zero;
                    anim.Play("Idle");
                }
                else
                {
                    Move();
                }

                Jump();
                Attack(KeyCode.J);
                Attack(KeyCode.K);
                
                break;
            default:
               
                break;
        }
        RecoverHpAndMp();

    }

    #region Update Func

    private void QuitJumpStatus()
    {
        if (isOnGround && unlockJumpResTimeCounter <= 0f)
        {
            playerStatus = PlayerStatus.Idle;
            anim.Play("Idle");
            isJumping  = false;
        }
        
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W) && !isJumping)
        {
            playerStatus = PlayerStatus.Jump;
            anim.Play("Jump");
            isJumping = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed) * multipleSlowDown;
            unlockJumpResTimeCounter = unlockJumpResTime;
        }
    }
    private void Attack(KeyCode keyCode)
    {
        
        if (Input.GetKeyDown(keyCode) && !equipmentController.isAttacking && !RhythmManager.Instance.inMusicMode)
        {
            rb.velocity = Vector2.zero;
            
            playerStatus = PlayerStatus.Attack;
            equipmentController.Attack(keyCode);
        }
       
    }

    private void Move()
    {
        if (moveDir == -1 && transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
        else if (moveDir == 1 && transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }

        if (Mathf.Abs(rb.velocity.x) <= moveSpeed)
        {
            rb.velocity = new Vector2(moveSpeed * moveDir, rb.velocity.y) * multipleSlowDown;

        }
    }




    private void RecoverHpAndMp()
    {
        if (mp != maxMp)
        {
            mpRecoverTimeCounter += Time.deltaTime;
            if (mpRecoverTimeCounter >= mpRecoverTime)
            {
                mpRecoverTimeCounter = 0;
                mp++;
                UIManager.Instance.GetPanelBase("PropPanel").GetComponent<PropPanel>().SetHeartUINum("Mp", mp);
            }
        }
    }
    
    

    #endregion
    #region Public API
    

    public void BeAttacked(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Debug.Log("You die");
        }
    }

    public void UseMp(int mpUsage)
    {
        mp -= mpUsage;
        UIManager.Instance.GetPanelBase("PropPanel").GetComponent<PropPanel>().SetHeartUINum("Mp",mp);
    }
    #endregion

    #region Event

    public event Action ActiveSkillEv;

    private void OnActiveSkill()
    {
        if (ActiveSkillEv != null)
            ActiveSkillEv();
    }


    #endregion

    #region Helper

    public void DebugReset()
    {
        playerStatus = PlayerStatus.Idle;
        isJumping  = false;
        anim.Play("Idle");
        equipmentController.isAttacking = false;
    }

    private void SetPlayerTimeScale(float multipleTime)
    {
        multipleSlowDown = GameManager.Instance.multipleSlowDown;
        
    }

    private void ResetPlayerTimeScale()
    {
        multipleSlowDown = 1f;
    }
    #endregion

    
}
