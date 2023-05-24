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


    #region 触发物品事件
    private Action<StuffEnum> showDetailOnUI;
    /// <summary>
    /// 展示物品交互内容
    /// </summary>
    public event Action<StuffEnum> ShowDetailOnUI
    {
        add => showDetailOnUI += value;
        remove => showDetailOnUI -= value;
    }

    private Action<StuffEnum> collectedInBackpack;
    /// <summary>
    /// 收集进背包的事件
    /// </summary>
    public event Action<StuffEnum> CollectedInBackpack
    {
        add => collectedInBackpack += value;
        remove => collectedInBackpack -= value;
    }
    #endregion

    /// <summary>
    /// 交互物品
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="stuffEnum"></param>
    public void Interaction(string msg, StuffEnum stuffEnum)
    {
        switch (stuffEnum)
        {
            case StuffEnum.Null:
                break;
            case StuffEnum.狗狗防疫本:
                break;
            case StuffEnum.座机:
                break;
            case StuffEnum.亮光的电脑:
                break;
            case StuffEnum.碗:
                break;
            case StuffEnum.暖壶:
                break;
            case StuffEnum.皮带狗绳:
            case StuffEnum.破破烂烂的的玩具:
            case StuffEnum.抽屉里的狗零食:
            case StuffEnum.狗骨头:
                //收集进背包
                collectedInBackpack?.Invoke(stuffEnum);
                break;
            case StuffEnum.台灯:
                break;
            case StuffEnum.门锁:
                break;
            case StuffEnum.ikun海报:
                break;
            case StuffEnum.门后的守则:
            case StuffEnum.公告栏:
            case StuffEnum.卫生间纸条:
            case StuffEnum.配电室牌:
                showDetailOnUI?.Invoke(stuffEnum);
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
