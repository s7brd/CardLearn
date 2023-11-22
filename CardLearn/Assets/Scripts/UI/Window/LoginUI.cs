using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 开始界面
/// </summary>
public class LoginUI : UIBase
{
    public void Awake()
    {
        //开始游戏
        Register("bg/startBtn").onClick = OnStartGameBtn;
    }

    private void OnStartGameBtn(GameObject obj, PointerEventData pData)
    {
        //关闭login界面
        UIManager.Instance.CloseUI("LoginUI");
        
        //战斗初始化
        FightManager.Instance.ChangeType(FightType.Init);
    }
    
}
