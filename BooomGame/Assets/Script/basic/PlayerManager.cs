using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 在这个脚本里的Triggered方法决定传什么
/// </summary>
public class PlayerManager : MonoBehaviour
{
    public delegate void TriggerEventHandler(string message,EGameObj gameItem);
    public static event TriggerEventHandler OnTrigger;
    
    public void Triggered(string message,EGameObj gameItem) {
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