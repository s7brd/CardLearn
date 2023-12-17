using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 失败
/// </summary>
public class Fight_Lose : FightUnit
{
    public override void Init()
    {
        Debug.Log("失败了");
        FightManager.Instance.StopAllCoroutines(); //将Fight_EnemyTurn中敌人行为的协程结束了
        
        //显示失败界面 看到这里的小伙伴可以自己制作了
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }
}
