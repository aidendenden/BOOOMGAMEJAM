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

    public Func<List<string>> getItemIDList;

    RectTransform rectTransform;
    bool openBackPack = false;
    private void Awake()
    {
        backpackListBtn?.onClick.AddListener(() => { OnBackpackBtnClick(getItemIDList?.Invoke()); });
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBackpackBtnClick(List<string> itemIdList)
    {
        if (openBackPack)
        {
            //DOTweenModuleUI
            rectTransform.DOAnchorPosX(-243.45f, 0.5f);
        }
        else
        {
            rectTransform.DOAnchorPosX(-0f, 0.5f);
        }
        

        for (int i = 0; i < itemIdList?.Count; i++)
        {

        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        openBackPack = false;
        OnBackpackBtnClick(getItemIDList?.Invoke());
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        openBackPack = true;
        OnBackpackBtnClick(getItemIDList?.Invoke());
    }
}
