using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BackpackStuffItemTrigger : MonoBehaviour, IPointerClickHandler
{
    public StuffEnum stuffEnum;
    public Image icon;

    public void SetStuffItemInfo(StuffInfo stuffInfo)
    {
        stuffEnum = stuffInfo.stuffType;
        icon.sprite = Sprite.Create(stuffInfo.stuffIcon, new Rect(0, 0, stuffInfo.stuffIcon.width, stuffInfo.stuffIcon.height), Vector2.zero);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        BackpackManager.Instance.OnBackpackItemViewClick(stuffEnum);
    }


}
