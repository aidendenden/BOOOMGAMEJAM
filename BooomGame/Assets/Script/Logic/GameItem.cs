using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 枚举是物品属性
/// 这个方法是广播进入事件
/// </summary>
public class GameItem : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] public StuffEnum gameItem;

    public bool OnTrigger;
    public bool OnClick;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (OnClick)
        {
            PlayerManager.Instance.Triggered("to touch", gameItem);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (OnTrigger)
        {
            PlayerManager.Instance.Triggered("to TriggerEnter", gameItem);
        }
    }
}