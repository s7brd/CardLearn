using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏入口脚本
/// </summary>
public class GameApp : MonoBehaviour
{
    private void Start()
    {
        //初始化配置表
        GameConfigManager.Instance.Init();
        
        //初始化用户信息
        RoleManager.Instatnce.Init();
        
        //初始化音频管理器
        AudioManager.Instance.Init();
        
        //显示loginUI 创建的脚本记得跟预制体物体名字一致
        UIManager.Instance.ShowUI<LoginUI>("LoginUI");
        
        //播放bgm
        AudioManager.Instance.PlayBGM("bgm1");
        
        //test
        string name = GameConfigManager.Instance.GetCardById("1001")["Name"];
        print(name);
    }
}
