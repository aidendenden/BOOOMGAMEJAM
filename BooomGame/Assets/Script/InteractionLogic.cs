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


    #region ������Ʒ�¼�
    private Action<StuffEnum> collectedInBackpack;
    /// <summary>
    /// �ռ����������¼�
    /// </summary>
    public event Action<StuffEnum> CollectedInBackpack
    {
        add => collectedInBackpack += value;
        remove => collectedInBackpack -= value;
    }
    #endregion

    /// <summary>
    /// ������Ʒ
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="stuffEnum"></param>
    public void Interaction(string msg, StuffEnum stuffEnum)
    {
        switch (stuffEnum)
        {
            case StuffEnum.Null:
                break;
            case StuffEnum.�������߱�:
                break;
            case StuffEnum.����:
                break;
            case StuffEnum.����ĵ���:
                break;
            case StuffEnum.��:
                break;
            case StuffEnum.ů��:
                break;
            case StuffEnum.Ƥ������:
            case StuffEnum.�������õĵ����:
            case StuffEnum.������Ĺ���ʳ:
            case StuffEnum.����ͷ:
                //�ռ�������
                collectedInBackpack?.Invoke(stuffEnum);
                break;
            case StuffEnum.̨��:
                break;
            case StuffEnum.����:
                break;
            case StuffEnum.ikun����:
                break;
            case StuffEnum.�ź������:
                break;
            case StuffEnum.������:
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
