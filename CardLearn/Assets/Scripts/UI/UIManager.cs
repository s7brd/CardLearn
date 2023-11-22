using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ui管理器
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private Transform canvasTf;  //画布的变换组件

    private List<UIBase> uiList; //存储加载过的界面的 集合
    
    private void Awake()
    {
        Instance = this;
        //找世界中的画布
        canvasTf = GameObject.Find("Canvas").transform;
        //初始化集合
        uiList = new List<UIBase>();
    }

    public UIBase ShowUI<T>(string uiName) where T : UIBase
    {
        UIBase ui = Find(uiName);
        if (ui == null)
        {
            //集合中没有 需要从Resources/UI文件夹中加载
            GameObject obj = Instantiate(Resources.Load("UI/" + uiName), canvasTf) as GameObject;
            
            //改名字
            obj.name = uiName;
            
            //添加需要的脚本
            ui = obj.AddComponent<T>();
            
            //添加到集合进行存储
            uiList.Add(ui);
        }
        else
        {
            //显示
            ui.Show();
        }

        return ui;
    }

    //隐藏
    public void HideUI(string uiName)
    {
        UIBase ui = Find(uiName);
        if (ui != null)
        {
            ui.Hide();
        }
    }

    //关闭全部界面
    public void CloseAllUI()
    {
        for (int i = uiList.Count - 1; i >= 0; i--)
        {
            uiList[i].Close();
            Destroy(uiList[i].gameObject);
        }
        uiList.Clear();
    }

    //关闭某个界面
    public void CloseUI(string uiName)
    {
        UIBase ui = Find(uiName);
        if (ui != null)
        {
            uiList.Remove(ui);
            ui.Close();
            Destroy(ui.gameObject);
        }
    }

    //从集合中找到名字对应的界面脚本
    public UIBase Find(string uiName)
    {
        for (int i = 0; i < uiList.Count; i++)
        {
            if (uiList[i].name == uiName)
            {
                return uiList[i];
            }   
        }

        return null;
    }
}
