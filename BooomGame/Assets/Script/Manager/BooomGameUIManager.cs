using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BooomGameUIManager : MonoBehaviour
{
    public static BooomGameUIManager Instance { get; private set; }

    public Slider alertBar;
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
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
}
