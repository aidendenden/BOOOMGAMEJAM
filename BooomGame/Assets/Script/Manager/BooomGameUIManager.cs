using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BooomGameUIManager : MonoBehaviour
{
    public static BooomGameUIManager Instance { get; private set; }

    public Slider alertBar;
    public Image stuffDetailsImage;
    public Button closeDetailsBtn;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);

        closeDetailsBtn.onClick.AddListener(OnCloseDetaiksBtnClick);
    }

    private void Start()
    {
        BackpackManager.Instance.OnBackpackStuffCheckClick = OnBackpackItemViewClick;
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

    /// <summary>
    /// 背包内物品点击
    /// </summary>
    /// <param name="stuffEnum"></param>
    public void OnBackpackItemViewClick(StuffEnum stuffEnum)
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
                
                break;
        }
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
        stuffDetailsImage.sprite = Sprite.Create(item.detailsPicture, new Rect(0, 0, item.detailsPicture.width, item.detailsPicture.height), Vector2.zero);

    }

    /// <summary>
    /// 关闭展示UI
    /// </summary>
    private void OnCloseDetaiksBtnClick()
    {
        stuffDetailsImage.DOFade(0, 0.2f);
        stuffDetailsImage.rectTransform.DOScale(0, 0.2f);
    }
}
