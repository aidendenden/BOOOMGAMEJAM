using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BackpackStuffItemTrigger : MonoBehaviour, /*IPointerClickHandler, */IPointerEnterHandler, IPointerExitHandler
{
    public StuffEnum stuffEnum;
    public Image icon;

    public Button check;

    private void Awake()
    {
        check?.onClick.AddListener(OnBackpackStuffClick);
        //check.gameObject.SetActive(false);
    }

    public void SetStuffItemInfo(StuffInfo stuffInfo)
    {
        stuffEnum = stuffInfo.stuffType;
        //icon.sprite = Sprite.Create(stuffInfo.stuffIcon, new Rect(0, 0, stuffInfo.stuffIcon.width, stuffInfo.stuffIcon.height), Vector2.zero);
        icon.sprite = stuffInfo.stuffIcon;
        if (stuffInfo.stuffType == StuffEnum.Null)
        {
            icon.color = new Color(1, 1, 1, 0);
        }
    }

    private void OnBackpackStuffClick()
    {
        Debug.Log("Backpack:\"" + stuffEnum + "\"--OnClick!");
        BackpackManager.Instance.OnBackpackStuffCheckClick?.Invoke(stuffEnum);
    }


    //public void OnPointerClick(PointerEventData eventData)
    //{
    //    BackpackManager.Instance.OnBackpackItemViewClick(stuffEnum);
    //}

    public void OnPointerExit(PointerEventData eventData)
    {
        //check.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //if (stuffEnum == StuffEnum.Null) return;
        //check.gameObject.SetActive(true);
    }
}
