using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 存储UI的信息以及路径
/// </summary>
public class UIType
{
    private string path;
    public string Path { get => path; }

    private string name;
    public string Name { get => name; }

    /// <summary>
    /// 只可通过在UiType在被实例化的时候，被传递然后给Path赋值
    /// </summary>
    /// <param name="ui_path">UI的路径</param>
    /// <param name="ui_name">UIPanel的名称</param>
    public UIType(string ui_path,string ui_name)
    {
        name = ui_name;
        path = ui_path;
    }

}
