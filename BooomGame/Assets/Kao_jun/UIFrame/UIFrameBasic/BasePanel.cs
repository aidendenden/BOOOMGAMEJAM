using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �洢UI��״̬��Ϣ
/// �ṩʵ�ֵĽӿ�
/// </summary>
public class BasePanel
{
    public UIType uiType;

    /// <summary>
    /// ���ڱ�UIManager��ȡ��Ȼ��Ϊ��󶨱��ص����壬�Ӷ����䱻BaseManager���ƣ�Ȼ��������ʵ���书��
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
