using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敌人回合
/// </summary>
public class Fight_EnemyTurn : FightUnit
{
    public override void Init()
    {
        //删除所有卡牌
        UIManager.Instance.GetUI<FightUI>("FightUI").RemoveAllCards();
        //显示敌人回合提示
        UIManager.Instance.ShowTip("玩家回合", Color.yellow, delegate
        {
            FightManager.Instance.StartCoroutine(EnemyManager.Instance.DoAllEnemyAction());
        });
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }
}
