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
}

public enum StuffEnum
{
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

