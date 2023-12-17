using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class CardItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Dictionary<string, string> data; //卡牌信息

    public void Init(Dictionary<string, string> data)
    {
        this.data = data;
    }

    private void Start()
    {
        transform.Find("bg").GetComponent<Image>().sprite = Resources.Load<Sprite>(data["BgIcon"]);
        transform.Find("bg/icon").GetComponent<Image>().sprite = Resources.Load<Sprite>(data["Icon"]);
        transform.Find("bg/msgTxt").GetComponent<Text>().text = string.Format(data["Des"], data["Arg0"]);
        transform.Find("bg/nameTxt").GetComponent<Text>().text = data["Name"];
        transform.Find("bg/useTxt").GetComponent<Text>().text = data["Expend"];
        transform.Find("bg/Text").GetComponent<Text>().text = GameConfigManager.Instance.GetCardTypeById(data["Type"])["Name"];
        
        //设置bg背景imgage的外边框材质
        transform.Find("bg").GetComponent<Image>().material = Instantiate(Resources.Load<Material>("Mats/outline"));
    }

    private int index;
    
    //鼠标进入
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(1.25f, 0.25f);
        index = transform.GetSiblingIndex();
        transform.SetAsLastSibling();
        
        transform.Find("bg").GetComponent<Image>().material.SetColor("_lineColor", Color.yellow);
        transform.Find("bg").GetComponent<Image>().material.SetFloat("_lineWidth", 10);
    }

    //鼠标离开
    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(1f, 0.25f);
        transform.SetSiblingIndex(index); //rrjw 后面研究下该函数细节
        
        transform.Find("bg").GetComponent<Image>().material.SetColor("_lineColor", Color.black);
        transform.Find("bg").GetComponent<Image>().material.SetFloat("_lineWidth", 1);
    }

    private Vector2 initPos; //拖拽开始时记录卡牌的 位置
    //开始拖拽
    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        initPos = transform.GetComponent<RectTransform>().anchoredPosition;
        
        //播放声音 rrjw 声音好像无效
        AudioManager.Instance.PlayEffect("Cards/draw");
    }

    //拖拽中
    public virtual void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                transform.parent.GetComponent<RectTransform>(),
                eventData.position,
                eventData.pressEventCamera,
                out pos))
        {
            transform.GetComponent<RectTransform>().anchoredPosition = pos;
        }
    }

    //结束拖拽
    public virtual void OnEndDrag(PointerEventData eventData)
    {
        transform.GetComponent<RectTransform>().anchoredPosition = initPos;
        transform.SetSiblingIndex(index);
    }

    //尝试使用卡牌
    public virtual bool TryUse()
    {
        //卡牌需要的费用
        int cost = int.Parse(data["Expend"]);
        if (cost > FightManager.Instance.CurPowerCount)
        {
            //费用不足
            AudioManager.Instance.PlayEffect("Effect/lose"); //使用失败音效
            
            //提示
            UIManager.Instance.ShowTip("费用不足", Color.red);

            return false;
        }
        else
        {
            //减少费用
            FightManager.Instance.CurPowerCount -= cost;
            //刷新费用文本
            UIManager.Instance.GetUI<FightUI>("FightUI").UpdatePower(); //rrjw 调用其他系统的ui或许用事件更合适 感觉后面应该加入事件系统总体设计设计
            //使用的卡牌删除
            UIManager.Instance.GetUI<FightUI>("FightUI").RemoveCard(this);

            return true;
        }
    }
    
    //创建卡牌
    public void PlayEffect(Vector3 pos)
    {
        GameObject effectObj = Instantiate(Resources.Load(data["Effects"])) as GameObject;
        effectObj.transform.position = pos;
        Destroy(effectObj, 2);
    }

}
