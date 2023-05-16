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
    /// 触发交互狗狗防疫本的事件
    /// </summary>
    private Action dogBook;
    /// <summary>
    /// 外部注册事件
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
            case StuffEnum.狗狗防疫本:
                dogBook?.Invoke();
                break;
            case StuffEnum.皮带狗绳:
                break;
            case StuffEnum.座机:
                break;
            case StuffEnum.亮光的电脑:
                break;
            case StuffEnum.碗:
                break;
            case StuffEnum.暖壶:
                break;
            case StuffEnum.破破烂烂的的玩具:
                break;
            case StuffEnum.台灯:
                break;
            case StuffEnum.门锁:
                break;
            case StuffEnum.ikun海报:
                break;
            case StuffEnum.抽屉里的狗零食:
                break;
            case StuffEnum.门后的守则:
                break;
            case StuffEnum.公告栏:
                break;
            case StuffEnum.狗骨头:
                break;
            case StuffEnum.Exit:
                break;
            case StuffEnum.宿舍门:
                break;
            case StuffEnum.死路门:
                break;
            case StuffEnum.宿舍自动开水机:
                break;
            case StuffEnum.垃圾:
                break;
            case StuffEnum.暖气片和袜子:
                break;
            case StuffEnum.晾衣叉子:
                break;
            case StuffEnum.密码锁:
                break;
            case StuffEnum.盆栽:
                break;
            default:
                break;
        }
    }
}
