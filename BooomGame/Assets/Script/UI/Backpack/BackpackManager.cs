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
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
            Instance = null;
        }
        Instance = this;

        //backpackItemsIDList.Add(StuffEnum.皮带狗绳);
        //backpackItemsIDList.Add(StuffEnum.破破烂烂的的玩具);
        //backpackItemsIDList.Add(StuffEnum.抽屉里的狗零食);
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
            if (!backpackItemsIDList.Contains(stuffEnum))
            {
                if (stuffEnum == StuffEnum.狗骨头)
                {
                   GameManager.Instance.isGetDogBone = true;
                }
                backpackItemsIDList.Add(stuffEnum);
            }
        }
        PlayAudioClip(pushIn);
    }
    private void OnDestroy()
    {
        InteractionLogic.Instance.CollectedInBackpack -= PushStuffInBackPack;
        Instance = null;
    }
    #endregion
}

