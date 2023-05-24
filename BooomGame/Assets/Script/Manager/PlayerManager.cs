using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 在这个脚本里的Triggered方法决定传什么
/// </summary>
[AddComponentMenu("GameInputManage")]
public class PlayerManager : MonoBehaviour
{
    private static readonly Lazy<PlayerManager> Lazy = new Lazy<PlayerManager>(() => new PlayerManager());

    private PlayerManager()
    {
    }

    public static PlayerManager Instance => Lazy.Value;

    public delegate void TriggerEventHandler(string message,StuffEnum gameItem);
    public static event TriggerEventHandler OnTrigger;
    
    public delegate void SoundEventHandler(string soundName,Transform transform);
    public static event SoundEventHandler OnPlaySound;
    
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

    public void PlaySound(string soundName,Transform transform)
    {
        Debug.Log("Playing sound: " + soundName);
        if (OnPlaySound != null)
            OnPlaySound(soundName,transform);
    }

    public void AddSoundListener(SoundEventHandler listener)
    {
        OnPlaySound += listener;
    }

    public void RemoveSoundListener(SoundEventHandler listener)
    {
        OnPlaySound -= listener;
    }
    
   

}

#region 监听交互的方法
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
#endregion