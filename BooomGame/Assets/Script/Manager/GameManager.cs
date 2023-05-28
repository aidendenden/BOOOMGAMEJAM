using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public sealed class GameManager : MonoBehaviour
{
    /// <summary>
    /// 开了一个线程锁以防万一 使用Lazy<T>类，它可以确保单例实例只在需要时创建，并且是线程安全的
    /// </summary>
    private static readonly Lazy<GameManager> Lazy = new Lazy<GameManager>(() => new GameManager());

    private GameManager()
    {
    }

    public static GameManager Instance
    {
        get { return Lazy.Value; }
    }

    /// <summary>
    /// 正确密码
    /// </summary>
    public const int CorrectPassword = 0451;

    /// <summary>
    /// 解锁密码锁状态 0:默认状态;1:正确;2:错误
    /// </summary>
    public static int UnlockState
    {
        set
        {
            if (value == 2)
            {
                ChangeAlertnessValue(500, null);
            }

            UnlockState = value;
        }
    }

    /// <summary>
    /// 是否在交互中 1:交互中;0:不在交互
    /// </summary>
    public static int IsInteracting = 0;

    public static float AlertnessMax = 100; //警觉度上限
    public static float AlertnessValue; //警觉度
    public static float AlertnessDownSpeed = 0.5f; //警觉度下降速度
    public static float BiaoZhunDistance = 15; //距离声源的标准距离

    public bool isGetDogBone;
    
    public static Transform SuGuant;


    #region 物品列表

    private static StuffList stuffList;

    /// <summary>
    /// 物品列表
    /// </summary>
    public static StuffList StuffList
    {
        get
        {
            if (stuffList == null)
            {
                stuffList = Resources.Load<StuffList>("SettingAsset/BooomGameStuffList");
            }

            return stuffList;
        }
    }

    #endregion


    public static void ChangeAlertnessValue(float value, Transform _t)
    {
        if (AlertnessValue < AlertnessMax)
        {
            float AlertnessWillAdd = value * GameManager.JudgmentDistance(_t);
            if (AlertnessValue + AlertnessWillAdd >= AlertnessMax)
            {
                AlertnessValue += AlertnessWillAdd;
                PlayerEventManager.Instance.Triggered("AlertnessValueHasChange", StuffEnum.Null, TriggerType.Null, _t);
            }
            else
            {
                AlertnessValue += AlertnessWillAdd;
            }
        }
    }

    public static float JudgmentDistance(Transform WuPinT)
    {
        SuGuant = GameObject.FindGameObjectWithTag("SuGuan").GetComponent<Transform>();
        float distance = Vector3.Distance(WuPinT.position, SuGuant.position);
        Debug.Log(BiaoZhunDistance / (BiaoZhunDistance / 2 + distance));
        return BiaoZhunDistance / (BiaoZhunDistance / 2 + distance);
    }
}


public enum StuffEnum
{
    Null,
    狗狗防疫本,
    皮带狗绳,
    座机,
    亮光的电脑,
    碗,
    暖壶,
    破破烂烂的的玩具,
    台灯,
    门锁,
    ikun海报,
    抽屉里的狗零食,
    门后的守则,
    公告栏,
    狗骨头,
    Exit,
    宿舍门,
    死路门,
    宿舍自动开水机,
    垃圾,
    暖气片和袜子,
    晾衣叉子,
    密码锁,
    盆栽,
    配电室牌,
    卫生间纸条,
    猫,
    电闸,
    洗手间
}

public enum TriggerType
{
    Null,
    调查,
    拉闸,
    收集
}