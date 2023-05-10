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
}

public enum StuffEnum
{
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

