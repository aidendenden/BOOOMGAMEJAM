using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackpackManager : MonoBehaviour
{
    public static BackpackManager Instance { get; private set; }

    public BackpackUIView backpackUIView;

    public AudioSource audioSource;
    public AudioClip open;
    public AudioClip close;
    public AudioClip pushIn;
    public AudioClip showDetail;


    public Action<StuffEnum> OnBackpackStuffCheckClick;

    #region UI内部代码
    private List<StuffEnum> backpackItemsIDList = new List<StuffEnum>();
    private void Awake()
    {
        Instance = this;

        backpackItemsIDList.Add(StuffEnum.皮带狗绳);
        backpackItemsIDList.Add(StuffEnum.破破烂烂的的玩具);
        backpackItemsIDList.Add(StuffEnum.抽屉里的狗零食);
        backpackUIView.getItemIDList = () => { return backpackItemsIDList; };

        InteractionLogic.Instance.CollectedInBackpack += PushStuffInBackPack;

        backpackUIView.OnOpen += () => PlayAudioClip(open);
        backpackUIView.OnClose += () => PlayAudioClip(close);
    }

    public void PlayAudioClip(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
    #endregion

    #region 外部调用

    public List<StuffEnum> BackpackStuff
    {
        get => backpackItemsIDList;
    }
    /// <summary>
    /// 将物品放进背包
    /// </summary>
    /// <param name="stuffEnum"></param>
    public void PushStuffInBackPack(StuffEnum stuffEnum)
    {
        if (!(backpackItemsIDList.Count >= 4))
        {
            backpackItemsIDList.Add(stuffEnum);
        }
        PlayAudioClip(pushIn);
    }
    
    #endregion
}

