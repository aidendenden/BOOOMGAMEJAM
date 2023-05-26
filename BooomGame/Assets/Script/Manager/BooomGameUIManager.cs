using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BooomGameUIManager : MonoBehaviour
{
    public static BooomGameUIManager Instance { get; private set; }

    public Slider alertBar;
    public Image stuffDetailsImage;
    public Button closeDetailsBtn;

    public GameObject stuffInfoContent;
    public TMP_Text stuffDetailInfo;
    public Button stuffDetailCloseBtn;

    public AudioSource audioSource;
    public AudioClip showDetail;
    public AudioClip exitDetail;
    
    public GameObject confirmPassword;

    private StuffEnum curStuff;
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);

        closeDetailsBtn.onClick.AddListener(() => CloseStuffDetail(curStuff));
        stuffDetailCloseBtn.onClick.AddListener(() => 
        {
            GameManager.IsInteracting = 0;
            stuffInfoContent.SetActive(false);
        });
    }

    private void Start()
    {
        BackpackManager.Instance.OnBackpackStuffCheckClick = OnBackpackItemViewClick;
        InteractionLogic.Instance.CheckStuff += ShowStuffDetail;

    }

    private void Update()
    {
        SetAlertBarValue(GameManager.AlertnessValue, GameManager.AlertnessMax);
    }

    /// <summary>
    /// 设置警觉条UI，最小值为0，最大值为1
    /// </summary>
    /// <param name="currentValue">当前数值</param>
    /// <param name="maxValue">计划最大数值</param>
    public void SetAlertBarValue(float currentValue, float maxValue = 1f)
    {
        float sliderValue = 0;
        if (currentValue > 0)
        {
            sliderValue = currentValue / maxValue;
        }
        alertBar.value = sliderValue;
    }

    #region 背包内点击事件
    /// <summary>
    /// 背包内物品点击
    /// </summary>
    /// <param name="stuffEnum"></param>
    private void OnBackpackItemViewClick(StuffEnum stuffEnum)
    {
        Debug.Log(stuffEnum + "--OnClicked!!!");
        switch (stuffEnum)
        {
            case StuffEnum.皮带狗绳:
            case StuffEnum.破破烂烂的的玩具:
            case StuffEnum.抽屉里的狗零食:
            case StuffEnum.狗骨头:
                var item = StuffInfo.GetStuffInfo(stuffEnum);
                if (item != null)
                {
                    StartCoroutine(ShowStuffDetails(item));
                }
                BackpackManager.Instance.PlayAudioClip(BackpackManager.Instance.showDetail);
                curStuff = stuffEnum;
                break;
        }
        GameManager.IsInteracting = 1;
    }
    /// <summary>
    /// 点击后根据UI状态进行展示
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    private IEnumerator ShowStuffDetails(StuffInfo item)
    {
        if (stuffDetailsImage.color.a == 1 || stuffDetailsImage.rectTransform.localScale == Vector3.one)
        {
            stuffDetailsImage.DOFade(0, 0.2f);
            stuffDetailsImage.rectTransform.DOScale(0, 0.2f);
            while (stuffDetailsImage.color.a != 0 || stuffDetailsImage.rectTransform.localScale != Vector3.zero) yield return null;
        }

        //stuffDetailsImage.gameObject.SetActive(true);

        stuffDetailsImage.DOFade(1, 0.5f);
        stuffDetailsImage.rectTransform.DOScale(1, 0.5f);
        //stuffDetailsImage.sprite = Sprite.Create(item.detailsPicture, new Rect(0, 0, item.detailsPicture.width, item.detailsPicture.height), Vector2.zero);
        stuffDetailsImage.sprite = item.detailsPicture;

    }

    /// <summary>
    /// 关闭展示UI
    /// </summary>
    private void OnCloseDetaiksBtnClick()
    {
        stuffDetailsImage.DOFade(0, 0.2f);
        stuffDetailsImage.rectTransform.DOScale(0, 0.2f);
        
    }
    #endregion

    #region 场景交互
    public void ShowStuffDetail(StuffEnum stuffEnum)
    {

        switch (stuffEnum)
        {
            case StuffEnum.门后的守则:
            case StuffEnum.公告栏:
            case StuffEnum.卫生间纸条:
            case StuffEnum.配电室牌:
                var item = StuffInfo.GetStuffInfo(stuffEnum);
                if (item != null)
                {
                    StartCoroutine(ShowStuffDetails(item));
                }
                audioSource.PlayOneShot(showDetail);
                curStuff = stuffEnum;
                break;
            case StuffEnum.狗狗防疫本:
            case StuffEnum.座机:
            case StuffEnum.亮光的电脑:
            case StuffEnum.碗:
            case StuffEnum.暖壶:
            case StuffEnum.台灯:
            case StuffEnum.门锁:
            case StuffEnum.ikun海报:
            case StuffEnum.Exit:
            case StuffEnum.宿舍门:
            case StuffEnum.死路门:
            case StuffEnum.宿舍自动开水机:
            case StuffEnum.垃圾:
            case StuffEnum.暖气片和袜子:
            case StuffEnum.晾衣叉子:
            case StuffEnum.盆栽:
                //展示文案
                var info = StuffInfo.GetStuffInfo(stuffEnum);
                if (info != null)
                {
                    StartCoroutine(ShowStuffDetailInfo(info));
                }
                break;
            case StuffEnum.密码锁:
                confirmPassword.SetActive(true);
                break;
        }
        GameManager.IsInteracting = 1;
    }
    public IEnumerator ShowStuffDetailInfo(StuffInfo item)
    {
        stuffInfoContent.SetActive(true);
        stuffDetailInfo.text = item.describe;
        GameManager.IsInteracting = 1;

        yield return new WaitForSeconds(10);

        stuffInfoContent.SetActive(false);
        GameManager.IsInteracting = 0;
    }

    public void CloseStuffDetail(StuffEnum stuffEnum)
    {
        OnCloseDetaiksBtnClick();
        switch (stuffEnum)
        {
            case StuffEnum.皮带狗绳:
            case StuffEnum.破破烂烂的的玩具:
            case StuffEnum.抽屉里的狗零食:
            case StuffEnum.狗骨头:
                BackpackManager.Instance.PlayAudioClip(BackpackManager.Instance.pushIn);
                break;
            default:
                audioSource.PlayOneShot(exitDetail);
                break;
        }
        curStuff = StuffEnum.Null;
        GameManager.IsInteracting = 0;
    }

    #endregion
}
