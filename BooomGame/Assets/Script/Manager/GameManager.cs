using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GameManager : MonoBehaviour
{
    /// <summary>
    /// 开了一个线程锁以防万一 使用Lazy<T>类，它可以确保单例实例只在需要时创建，并且是线程安全的
    /// </summary>
    private static readonly Lazy<GameManager> lazy = new Lazy<GameManager>(() => new GameManager());

    private GameManager()
    {
    }

    public static GameManager Instance
    {
        get { return lazy.Value; }
    }

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

    // 以下是监听交互的方法
    // void OnEnable()
    // {
    //     PlayerManager.OnTrigger += HandleTrigger;
    // }
    //
    // void OnDisable()
    // {
    //     PlayerManager.OnTrigger -= HandleTrigger;
    // }
    //
    // void HandleTrigger(string message,EGameObj gameObj)
    // {
    //     Debug.Log("Trigger event received:222222222222 " + message,StuffEnum);
    // }
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
    盆栽
}
