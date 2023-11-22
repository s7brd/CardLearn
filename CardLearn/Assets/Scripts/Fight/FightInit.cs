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
        AudioManager.Instance.PlayBGM("battle");
        //显示展读界面
        UIManager.Instance.ShowUI<FightUI>("FightUI");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }
}
