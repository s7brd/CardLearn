using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 卡牌战斗初始化
/// </summary>
public class FightInit : FightUnit
{
    public override void Init()
    {
        base.Init();
        //初始化战斗数值
        FightManager.Instance.Init();
        
        //切换bgm
        AudioManager.Instance.PlayBGM("battle");
        
        //敌人生成
        EnemyManager.Instance.LoadRes("10003"); //这里读取关卡3的敌人信息,可以自由选择
        
        //初始化战斗卡牌
        FightCardManager.Instance.Init();
        
        //显示展读界面
        UIManager.Instance.ShowUI<FightUI>("FightUI");
        
        //切换到玩家回合
        FightManager.Instance.ChangeType(FightType.Player);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }
}
