using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

public class GameManager : UnitySingleton<GameManager>
{
    [ShowInInspector] public Dictionary<string, Skill> DicSkill = new Dictionary<string, Skill>();
    public PlayerController playerController;
    public float multipleTime;
    public float multipleSlowDown;
    public Vector3 respawnV3;

    private void Awake()
    {
        Initialization();
    }

    private void Start()
    {
        SetTimeScaleEv += SetGMTimeScale;
        ResetTimeScaleEv += ResetGMTimeScale;
    }



    #region Public API

    public event Action<float> SetTimeScaleEv;
    public void OnSetTimeScaleEv(float multipleTime)
    {
        SetTimeScaleEv?.Invoke(multipleTime);
    }

    public event Action ResetTimeScaleEv;

    public void OnResetTimeScaleEv()
    {
        if (ResetTimeScaleEv != null)
        {
            ResetTimeScaleEv();
        }
    }
    
    
    #endregion

    
   #region Dictionary Func

   public void DicAdd<T1,T2>(Dictionary<T1, T2> dic, T1 tkey, T2 tValue)
   {
       dic.Add(tkey, tValue);
       //TODO: UI
   }

   public void DicRemove<T1,T2>(Dictionary<T1, T2> dic, T1 tkey)
   {
       dic.Remove(tkey);
   }

   public void FindSkill(string skillName,out Skill skill)
   {
       Debug.Log("Finding");
       DicSkill.TryGetValue(skillName, out skill);
       Debug.Log(skill);
   }

   public void SetRespawn(Vector3 v3)
   {
       respawnV3 = v3;
   }
   #endregion

   #region Private

   private void SetGMTimeScale(float multipleTime)
   {
       this.multipleTime = multipleTime;
       //对所有非note的对象减速4倍
        
       Animator[] anims = GameObject.FindObjectsOfType<Animator>();
       for (int idx = 0; idx < anims.Length; idx++)
       {
           anims[idx].speed *= multipleSlowDown;
       }

       Rigidbody2D[] rbs = GameObject.FindObjectsOfType<Rigidbody2D>();
       Debug.Log(rbs.Length);
       for (int idx = 0; idx < rbs.Length; idx++)
       {
           rbs[idx].velocity *= multipleSlowDown;
       }

   }

   private void ResetGMTimeScale()
   {
       this.multipleTime = 1f;
       Animator[] anims = GameObject.FindObjectsOfType<Animator>();
       for (int idx = 0; idx < anims.Length; idx++)
       {
           anims[idx].speed = 1f;
       }
       Rigidbody2D[] rbs = GameObject.FindObjectsOfType<Rigidbody2D>();
       for (int idx = 0; idx < rbs.Length; idx++)
       {
           rbs[idx].velocity /= multipleSlowDown;
       }
   }
   private void Initialization()
   {
       playerController = GameObject.Find("Player").GetComponent<PlayerController>();
   }

   #endregion
 
}
