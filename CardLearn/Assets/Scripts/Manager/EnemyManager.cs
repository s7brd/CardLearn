using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敌人管理器
/// </summary>
public class EnemyManager
{
    public static EnemyManager Instance = new EnemyManager();

    private List<Enemy> enemyList; //存储战斗中的敌人
    
    /// <summary>
    /// 加载敌人资源
    /// </summary>
    /// <param name="id">关卡id</param>
    public void LoadRes(string id)
    {

        enemyList = new List<Enemy>();
        
        //读取关卡表
        Dictionary<string, string> levelData = GameConfigManager.Instance.GetLevelById(id);
        
        //敌人id信息
        string[] enemyIds = levelData["EnemyIds"].Split("=");

        string[] enemyPos = levelData["Pos"].Split("="); //敌人位置

        for (int i = 0; i < enemyIds.Length; i++)
        {
            string enemyId = enemyIds[i];
            string[] posArr = enemyPos[i].Split(",");
            //敌人位置
            float x = float.Parse(posArr[0]);
            float y = float.Parse(posArr[1]);
            float z = float.Parse(posArr[2]);
            
            
            //根据敌人id 获得单个敌人信息
            Dictionary<string, string> enemyData = GameConfigManager.Instance.GetEnemyById(enemyId);
            
            GameObject obj = Object.Instantiate(Resources.Load(enemyData["Model"])) as GameObject; //从资源路径加载对应的敌人模型

            Enemy enemy = obj.AddComponent<Enemy>(); //添加敌人脚本
            
            enemy.Init(enemyData); //存储敌人信息
            
            //存储到集合
            enemyList.Add(enemy);
            
            obj.transform.position = new Vector3(x, y, z);
        }
    }

    //移除敌人
    public void DeletEnemy(Enemy enemy)
    {
        enemyList.Remove(enemy);
        
        //后续做是否击杀所有怪物判断
        if (enemyList.Count == 0)
        {
            FightManager.Instance.ChangeType(FightType.Win);
        }
    }
    
    //执行活着的怪物的行为
    public IEnumerator DoAllEnemyAction()
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            yield return FightManager.Instance.StartCoroutine(enemyList[i].DoAction());
        }
        //行动完后 更新所有敌人行为
        for (int i = 0; i < enemyList.Count; i++)
        {
            enemyList[i].SetRandomAction();
        }
        
        //切换到玩家回合
        FightManager.Instance.ChangeType(FightType.Player);
    }
}
