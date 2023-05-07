using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 存储UI的状态信息
/// 提供实现的接口
/// </summary>
public class BasePanel
{
    public UIType uiType;

    /// <summary>
    /// 用于被UIManager获取，然后为其绑定本地的物体，从而让其被BaseManager控制，然后在子类实现其功能
    /// </summary>
    public GameObject ActiveObj;

    public BasePanel(UIType uitype) 
    {
        uiType = uitype;
    }

    public virtual void OnStart() 
    {
        Debug.Log("Obj is loaded!");
        if (ActiveObj.GetComponent<CanvasGroup>() == null)  
        {
            ActiveObj.AddComponent<CanvasGroup>();
        }
    }

    public virtual void OnEnable() 
    {
        UIMethods.GetInstance().AddOrGetComponent<CanvasGroup>(ActiveObj).interactable = true ;
    }

    public virtual void OnDisable() 
    {
        UIMethods.GetInstance().AddOrGetComponent<CanvasGroup>(ActiveObj).interactable = false;
    }

    public virtual void OnDestory() 
    {
        UIMethods.GetInstance().AddOrGetComponent<CanvasGroup>(ActiveObj).interactable = false;
    }

}
