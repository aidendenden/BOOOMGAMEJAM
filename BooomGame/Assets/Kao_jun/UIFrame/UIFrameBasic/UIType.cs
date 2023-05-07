using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// �洢UI����Ϣ�Լ�·��
/// </summary>
public class UIType
{
    private string path;
    public string Path { get => path; }

    private string name;
    public string Name { get => name; }

    /// <summary>
    /// ֻ��ͨ����UiType�ڱ�ʵ������ʱ�򣬱�����Ȼ���Path��ֵ
    /// </summary>
    /// <param name="ui_path">UI��·��</param>
    /// <param name="ui_name">UIPanel������</param>
    public UIType(string ui_path,string ui_name)
    {
        name = ui_name;
        path = ui_path;
    }

}
