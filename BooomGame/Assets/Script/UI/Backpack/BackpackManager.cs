using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackpackManager : MonoBehaviour
{
    public static BackpackManager Instance { get; private set; }

    public BackpackUIView backpackUIView;
    public Action<StuffEnum> OnBackpackStuffCheckClick;

    #region UI�ڲ�����
    private List<StuffEnum> backpackItemsIDList = new List<StuffEnum>();
    private void Awake()
    {
        Instance = this;

        //backpackItemsIDList.Add(StuffEnum.Ƥ������);
        //backpackItemsIDList.Add(StuffEnum.�������õĵ����);
        //backpackItemsIDList.Add(StuffEnum.������Ĺ���ʳ);
        backpackUIView.getItemIDList = () => { return backpackItemsIDList; };

        InteractionLogic.Instance.CollectedInBackpack += PushStuffInBackPack;
    }

    #endregion

    #region �ⲿ����

    public List<StuffEnum> BackpackStuff
    {
        get => backpackItemsIDList;
    }
    /// <summary>
    /// ����Ʒ�Ž�����
    /// </summary>
    /// <param name="stuffEnum"></param>
    public void PushStuffInBackPack(StuffEnum stuffEnum)
    {
        if (!(backpackItemsIDList.Count >= 4))
        {
            backpackItemsIDList.Add(stuffEnum);
        }
        
    }
    
    #endregion

}
