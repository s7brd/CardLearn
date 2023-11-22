using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 界面基类
/// </summary>
public class UIBase : MonoBehaviour
{
    public UIEventTrigger Register(string name)
    {
        Transform tran = transform.Find(name);
        return UIEventTrigger.Get(tran.gameObject);
    }
    
    //显示
    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
    
    //关闭界面(销毁)
    public virtual void Close()
    {
    }
}
