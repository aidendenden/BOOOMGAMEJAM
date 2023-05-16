using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionLogic
{
    private static InteractionLogic instance;
    public static InteractionLogic Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new InteractionLogic();
            }
            return instance;
        }
    }

    private InteractionLogic() 
    {
        PlayerManager.Instance.AddListener(Interaction);
    }

    /// <summary>
    /// ���������������߱����¼�
    /// </summary>
    private Action dogBook;
    /// <summary>
    /// �ⲿע���¼�
    /// </summary>
    public event Action DogBook
    {
        add => dogBook += value;
        remove => dogBook -= value;
    }


    public void Interaction(string msg, StuffEnum stuffEnum)
    {
        switch (stuffEnum)
        {
            case StuffEnum.Null:
                break;
            case StuffEnum.�������߱�:
                dogBook?.Invoke();
                break;
            case StuffEnum.Ƥ������:
                break;
            case StuffEnum.����:
                break;
            case StuffEnum.����ĵ���:
                break;
            case StuffEnum.��:
                break;
            case StuffEnum.ů��:
                break;
            case StuffEnum.�������õĵ����:
                break;
            case StuffEnum.̨��:
                break;
            case StuffEnum.����:
                break;
            case StuffEnum.ikun����:
                break;
            case StuffEnum.������Ĺ���ʳ:
                break;
            case StuffEnum.�ź������:
                break;
            case StuffEnum.������:
                break;
            case StuffEnum.����ͷ:
                break;
            case StuffEnum.Exit:
                break;
            case StuffEnum.������:
                break;
            case StuffEnum.��·��:
                break;
            case StuffEnum.�����Զ���ˮ��:
                break;
            case StuffEnum.����:
                break;
            case StuffEnum.ů��Ƭ������:
                break;
            case StuffEnum.���²���:
                break;
            case StuffEnum.������:
                break;
            case StuffEnum.����:
                break;
            default:
                break;
        }
    }
}
