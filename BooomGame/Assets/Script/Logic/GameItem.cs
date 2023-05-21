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
    [SerializeField]
    public StuffEnum gameItem;

    public void OnPointerClick(PointerEventData eventData)
    {
        PlayerManager.Instance.Triggered("to touch",gameItem);
    }

    void OnTriggerEnter(Collider other) {
        PlayerManager triggerEvent = other.GetComponent<PlayerManager>();
        if (triggerEvent != null) {
            triggerEvent.Triggered("Hello, World!",gameItem);
        }
    }
    
}
