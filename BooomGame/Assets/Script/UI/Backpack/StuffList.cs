using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//namespace BooomGame.StuffList
//{
//}
[CreateAssetMenu(menuName = "BooomGame/StuffListScriptableObject")]
public class StuffList : ScriptableObject
{
    public List<StuffInfo> stuffList;
}

[Serializable]
public class StuffInfo
{
    [Tooltip("物品类型")]
    public StuffEnum stuffType;
    [Tooltip("物品名称")]
    public string stuffName;
    [Tooltip("物品描述")]
    public string describe;
    [Tooltip("物品图标")]
    public Texture2D stuffIcon;
    [Tooltip("能否收集进入背包")]
    public bool canCollectedInBackpack;

    /// <summary>
    /// 根据物品枚举查找物品信息
    /// </summary>
    /// <param name="stuffEnum"></param>
    /// <returns></returns>
    public static StuffInfo GetStuffInfo(StuffEnum stuffEnum)
    {
        StuffInfo stuffInfo = null;
        GameManager.StuffList.stuffList.ForEach((item) =>
        {
            if (item.stuffType.Equals(stuffEnum))
            {
                stuffInfo = item;
            }
        });
        return stuffInfo;
    }
    /// <summary>
    /// 获取一个空物品信息
    /// </summary>
    /// <returns></returns>
    public static StuffInfo ReturnNullStuffInfo()
    {
        return new StuffInfo()
        {
            stuffType = StuffEnum.Null,
            stuffName = null,
            describe = null,
            stuffIcon = new Texture2D(1, 1),
            canCollectedInBackpack = false,
        };
    }
}

public enum StuffEnum
{
    Null,
    狗狗防疫本,
    皮带狗绳,
    座机,
    亮光的电脑,
    碗,
    暖壶,
    破破烂烂的的玩具,
    台灯,
    门锁,
    ikun海报,
    抽屉里的狗零食,
    门后的守则,
    公告栏,
    狗骨头,
    Exit,
    宿舍门,
    死路门,
    宿舍自动开水机,
    垃圾,
    暖气片和袜子,
    晾衣叉子,
    密码锁,
    盆栽
}

