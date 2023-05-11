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
    public Button backpackListBtn;
    public RectTransform backpackListContent;
    public List<BackpackStuffItemView> backpackStuffItemViews;

    public Func<List<StuffEnum>> getItemIDList;

    RectTransform rectTransform;
    bool openBackPack = false;
    private void Awake()
    {
        backpackListBtn?.onClick.AddListener(() => { ShowOrHideBackpack(); });
        rectTransform = GetComponent<RectTransform>();
    }

    private void ShowOrHideBackpack()
    {
        if (openBackPack)
        {
            rectTransform.DOAnchorPosX(-backpackListContent.sizeDelta.x, 0.5f);
            RefreshBackContent(getItemIDList?.Invoke());
        }
        else
        {
            rectTransform.DOAnchorPosX(-0f, 0.5f);
        }
    }

    private void RefreshBackContent(List<StuffEnum> itemIdList)
    {
        for (int i = 0; i < backpackStuffItemViews?.Count; i++)
        {
            
            if (itemIdList.Count > 0 && itemIdList.Count -1 <= i)
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

    #region Êó±êÊÂ¼þ
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
