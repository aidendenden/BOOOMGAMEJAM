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
    private GameManager() { }
    
    public static GameManager Instance
    {
        get
        {
            return lazy.Value;
        }
    }

    /// <summary>
    /// 单例变量，想要传什么往下面塞
    /// </summary>
    public string test;

}
