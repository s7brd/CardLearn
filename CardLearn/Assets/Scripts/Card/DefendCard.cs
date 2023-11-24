using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 防御卡(加护甲)
/// </summary>
public class DefendCard : CardItem
{
    public override void OnEndDrag(PointerEventData eventData) //rrjw 其实应该增加一个使用接口,而不是把拖动写到这里
    {
        if (TryUse())
        {
            //使用效果
            int val = int.Parse(data["Arg0"]);
            
            //播放使用后的声音(每张卡使用的声音可能不一样)
            AudioManager.Instance.PlayEffect("Effect/healspell"); //这个字段可以配置到表中(作者说自己没设计好)
            
            //增加防御力
            FightManager.Instance.DefenseCount += val;
            //刷新防御文本
            UIManager.Instance.GetUI<FightUI>("FightUI").UpdateDefense(); //rrjw,这里脚本里该数据,实现处监听变化会更好些

            Vector3 pos = Camera.main.transform.position; //rrjw 这个不是引用? 对相机位置无影响? 只读? 后续研究下
            pos.y = 0;
            PlayEffect(pos);
        }else
        {
            
        }
    }
}
