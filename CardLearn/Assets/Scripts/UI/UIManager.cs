using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

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

    //获得某个界面的脚本
    public T GetUI<T>(string uiName) where T : UIBase //rrjw 后面研究下相关用法
    {
        UIBase ui = Find(uiName);
        if (ui != null)
        {
            return ui.GetComponent<T>();
        }

        return null;
    }

    //创建敌人头部
    public GameObject CreateActionIcon()
    {
        GameObject obj = Instantiate(Resources.Load("UI/actionIcon"), canvasTf) as GameObject;
        obj.transform.SetAsFirstSibling(); //设置在父级的最后一位
        return obj;
    }
    
    //创建敌人底部的血量物体
    public GameObject CreateHpItem()
    {
        GameObject obj = Instantiate(Resources.Load("UI/HpItem"), canvasTf) as GameObject;
        obj.transform.SetAsFirstSibling(); //设置在父级的最后一位
        return obj;
    }
    
    //提示界面
    public void ShowTip(string msg, Color color, System.Action callback = null)
    {
        //rrjw 可以优化成固定在那里的
        GameObject obj = Instantiate(Resources.Load("UI/Tips"), canvasTf) as GameObject;
        Text text = obj.transform.Find("bg/Text").GetComponent<Text>();
        text.color = color;
        text.text = msg;
        Tween scale1 = obj.transform.Find("bg").DOScaleY(1, 0.2f);
        Tween scale2 = obj.transform.Find("bg").DOScaleY(0, 0.2f);

        Sequence seq = DOTween.Sequence();
        seq.Append(scale1);
        seq.AppendInterval(0.5f);
        seq.Append(scale2);
        seq.AppendCallback(delegate
        {
            if (callback != null)
            {
                callback();
            }
            MonoBehaviour.Destroy(obj);
        });
    }
}
