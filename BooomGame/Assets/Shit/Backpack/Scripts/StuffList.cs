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
    [Tooltip("��Ʒ����")]
    public StuffEnum stuffType;
    [Tooltip("��Ʒ����")]
    public string stuffName;
    [Tooltip("��Ʒ����")]
    public string describe;
    [Tooltip("��Ʒͼ��")]
    public Texture2D stuffIcon;
    [Tooltip("�ܷ��ռ����뱳��")]
    public bool canCollectedInBackpack;

    /// <summary>
    /// ������Ʒö�ٲ�����Ʒ��Ϣ
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
    /// ��ȡһ������Ʒ��Ϣ
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
    �������߱�,
    Ƥ������,
    ����,
    ����ĵ���,
    ��,
    ů��,
    �������õĵ����,
    ̨��,
    ����,
    ikun����,
    ������Ĺ���ʳ,
    �ź������,
    ������,
    ����ͷ,
    Exit,
    ������,
    ��·��,
    �����Զ���ˮ��,
    ����,
    ů��Ƭ������,
    ���²���,
    ������,
    ����
}

