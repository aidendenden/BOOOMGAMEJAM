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
    
    

    /// <summary>
    /// 物品列表
    /// </summary>
    public static StuffList StuffList;
    /// 以下是监听交互的方法
    /// </summary>
    void OnEnable()
    {
        PlayerManager.OnTrigger += HandleTrigger;
    }
    
    void OnDisable()
    {
        PlayerManager.OnTrigger -= HandleTrigger;
    }
    
    void HandleTrigger(string message,EGameObj gameObj)
    {
        Debug.Log("Trigger event received:222222222222 " + message);
    }
}


/// <summary>
/// 游戏道具类型
/// </summary>
public enum EGameObj
{
    door,
    box,
}