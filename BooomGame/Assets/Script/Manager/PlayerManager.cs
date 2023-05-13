using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 在这个脚本里的Triggered方法决定传什么
/// </summary>
public class PlayerManager : MonoBehaviour
{
    private static readonly Lazy<PlayerManager> Lazy = new Lazy<PlayerManager>(() => new PlayerManager());

    private PlayerManager()
    {
    }

    public static PlayerManager Instance => Lazy.Value;

    public delegate void TriggerEventHandler(string message,StuffEnum gameItem);
    public static event TriggerEventHandler OnTrigger;
    
    public void Triggered(string message,StuffEnum gameItem) {
        Debug.Log("Triggered: " + message);
        if (OnTrigger != null)
            OnTrigger(message,gameItem);
    }

    public void AddListener(TriggerEventHandler listener) {
        OnTrigger += listener;
    }

    public void RemoveListener(TriggerEventHandler listener) {
        OnTrigger -= listener;
    }
    
}