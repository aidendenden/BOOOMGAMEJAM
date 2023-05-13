using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

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
                stuffList = Resources.Load<StuffList>("BooomGameStuffList");
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

