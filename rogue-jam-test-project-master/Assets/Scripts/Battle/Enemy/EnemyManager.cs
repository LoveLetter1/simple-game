using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

public class EnemyManager : SerializedMonoBehaviour
{
    [ShowInInspector]
    public Dictionary<string, GameObject> DicEnemyObj = new Dictionary<string, GameObject>();

    public List<Enemy> enemies;
    private void Start()
    {
        GameManager.Instance.SetTimeScaleEv += SetEnemyTimeScale;
        GameManager.Instance.ResetTimeScaleEv += ResetEnemyTimeScale;
        var trans = GetComponentsInChildren<Transform>();
        for (int idx = 1; idx < trans.Length; idx++)
        {
            CreateEnemy("Enemy01", trans[idx].position);

        }
    }

    public void CreateEnemy(string enemyName, Vector3 pos)
    {
        GameObject enemyObj = Instantiate(DicEnemyObj[enemyName], pos, default, transform);
        Enemy enemy = enemyObj.GetComponent<global::Enemy>();
        enemy.CreateInit();
        enemies.Add(enemy);
    }

    public void SetEnemyTimeScale(float multipleTime)
    {
        var multipleSlowDown = GameManager.Instance.multipleSlowDown;
        for (int idx = 0; idx < enemies.Count; idx++)
        {
            enemies[idx].multipleSlowDown = multipleSlowDown;
        }
    }

    public void ResetEnemyTimeScale()
    {   
        for (int idx = 0; idx < enemies.Count; idx++)
        {
            enemies[idx].multipleSlowDown = 1f;
        }
    }

}

