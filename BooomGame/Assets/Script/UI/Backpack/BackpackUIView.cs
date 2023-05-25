using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BackpackUIView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public List<BackpackStuffItemTrigger> backpackStuffItemViews;

    public Func<List<StuffEnum>> getItemIDList;

    RectTransform rectTransform;
    bool openBackPack = false;

    private Action onOpen;
    public event Action OnOpen
    {
        add => onOpen += value;
        remove => onOpen -= value;
    }
    private Action onClose;
    public event Action OnClose
    {
        add => onClose += value;
        remove => onClose -= value;
    }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void ShowOrHideBackpack()
    {
        if (openBackPack)
        {
            onOpen?.Invoke();
            rectTransform.DOAnchorPosX(-rectTransform.sizeDelta.x, 0.5f);
            RefreshBackContent(getItemIDList?.Invoke());
        }
        else
        {
            onClose?.Invoke();
            rectTransform.DOAnchorPosX(0f, 0.5f);
        }
    }

    private void RefreshBackContent(List<StuffEnum> itemIdList)
    {
        for (int i = 0; i < backpackStuffItemViews?.Count; i++)
        {
            
            if (itemIdList.Count > 0 && itemIdList.Count -1 >= i)
            {
                StuffInfo stuffInfo = StuffInfo.GetStuffInfo(itemIdList[i]);
                backpackStuffItemViews[i].SetStuffItemInfo(stuffInfo);
            }
            else
            {
                StuffInfo stuffInfo = StuffInfo.ReturnNullStuffInfo();
                backpackStuffItemViews[i].SetStuffItemInfo(stuffInfo);
            }
        }
        //for (int i = 0; i < itemIdList?.Count; i++)
        //{
            
        //    if (backpackStuffItemViews.Count - 1 <)
        //    {

        //    }
        //}
    }

    #region 鼠标事件
    public void OnPointerExit(PointerEventData eventData)
    {
        openBackPack = false;
        ShowOrHideBackpack();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        openBackPack = true;
        ShowOrHideBackpack();
    }
    #endregion
}
