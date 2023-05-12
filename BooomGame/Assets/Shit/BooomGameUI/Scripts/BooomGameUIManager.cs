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
}
