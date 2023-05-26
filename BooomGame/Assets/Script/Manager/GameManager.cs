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

    
    [FormerlySerializedAs("WatchfulnessMax")] public float AlertnessMax = 100;//警觉度上限

    [FormerlySerializedAs("watchfulnessNow")] public float AlertnessValue;
    [FormerlySerializedAs("watchfulnessDownSpeed")] public float AlertnessDownSpeed = 1;//警觉度下降速度
    
    
    #region 物品列表
    private static StuffList stuffList;
    /// <summary>
    /// 物品列表
    /// </summary>
    public static StuffList StuffList 
    {
        get 
        {
            if (stuffList == null )
            {
                stuffList = Resources.Load<StuffList>("SettingAsset/BooomGameStuffList");
            }
            return stuffList;
        }
    }
    #endregion


    public void ChangeAlertnessValue(int value)
    {
        AlertnessValue += value;
        PlayerEventManager.Instance.Triggered("AlertnessValueHasChange",StuffEnum.Null,TriggerType.Null,null);
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
    卫生间纸条
}

public enum TriggerType
{
    Null,
    调查,
    拉闸,
    收集
}
