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

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        BackpackManager.Instance.OnBackpackStuffCheckClick = OnBackpackItemViewClick;
    }
    /// <summary>
    /// ���þ�����UI����СֵΪ0�����ֵΪ1
    /// </summary>
    /// <param name="currentValue">��ǰ��ֵ</param>
    /// <param name="maxValue">�ƻ������ֵ</param>
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
    /// ��������Ʒ���
    /// </summary>
    /// <param name="stuffEnum"></param>
    public void OnBackpackItemViewClick(StuffEnum stuffEnum)
    {
        Debug.Log(stuffEnum + "--OnClicked!!!");
        switch (stuffEnum)
        {
            case StuffEnum.Ƥ������:
            case StuffEnum.�������õĵ����:
            case StuffEnum.������Ĺ���ʳ:
            case StuffEnum.����ͷ:
                var item = StuffInfo.GetStuffInfo(stuffEnum);
                if (item != null)
                {
                    StartCoroutine(ShowStuffDetails(item));
                }
                
                break;
        }
    }

    /// <summary>
    /// ��������UI״̬����չʾ
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
}
