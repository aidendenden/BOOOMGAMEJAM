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
    private Action<StuffEnum> checkStuff;
    /// <summary>
    /// 展示物品交互内容
    /// </summary>
    public event Action<StuffEnum> CheckStuff
    {
        add => checkStuff += value;
        remove => checkStuff -= value;
    }

    private Action<StuffEnum> closes;
    /// <summary>
    /// 拉闸
    /// </summary>
    public event Action<StuffEnum> Closes
    {
        add => closes += value;
        remove => closes -= value;
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
            case StuffEnum.皮带狗绳:
            case StuffEnum.破破烂烂的的玩具:
            case StuffEnum.抽屉里的狗零食:
            case StuffEnum.狗骨头:
                //收集进背包
                collectedInBackpack?.Invoke(stuffEnum);
                break;
            case StuffEnum.门后的守则:
            case StuffEnum.公告栏:
            case StuffEnum.卫生间纸条:
            case StuffEnum.配电室牌:
                checkStuff?.Invoke(stuffEnum);
                break;
            case StuffEnum.狗狗防疫本:
            case StuffEnum.座机:
            case StuffEnum.亮光的电脑:
            case StuffEnum.碗:
            case StuffEnum.暖壶:
            case StuffEnum.台灯:
            case StuffEnum.门锁:
            case StuffEnum.ikun海报:
            case StuffEnum.Exit:
            case StuffEnum.宿舍门:
            case StuffEnum.死路门:
            case StuffEnum.宿舍自动开水机:
            case StuffEnum.垃圾:
            case StuffEnum.暖气片和袜子:
            case StuffEnum.晾衣叉子:
            case StuffEnum.密码锁:
            case StuffEnum.盆栽:
                checkStuff?.Invoke(stuffEnum);
                break;
            default:
                break;
        }
    }


}
