using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��UIMethod�����ɵ���ģʽ
/// �䷽�������ṩ��Panelʹ�õ�
/// </summary>
public class UIMethods 
{
    private static UIMethods instance;
    public static UIMethods GetInstance() 
    {
        if (instance == null)  
        {
            instance = new UIMethods();    
        }
        return instance;
    }

    
    /// <summary>
    /// ��Ŀ������и��������ҵ��Ӷ���
    /// </summary>
    /// <param name="Find_Panel">Ŀ��Panel</param>
    /// <param name="Find_Name">Ŀ���Ӷ�������</param>
    /// <returns></returns>
    public GameObject FindObjectInChild(GameObject Find_Panel, string Find_Name) 
    {
        //ע��Ҫ��s
        Transform[] transforms_find = Find_Panel.GetComponentsInChildren<Transform>();

        foreach (Transform tra in transforms_find) 
        {
            if (tra.gameObject.name==Find_Name) 
            {
                return tra.gameObject;
                break;
            }
        }

        Debug.LogWarning($"û����{Find_Panel.name}���ҵ�{Find_Name}���壡");
        return null;
    }

    /// <summary>
    /// ��õ�ǰ�����е�Canvas
    /// </summary>
    /// <returns>Canvas Object</returns>
    public GameObject FindCanvas() 
    {
        GameObject gameObject_canvas = GameObject.FindObjectOfType<Canvas>().gameObject;
        if (gameObject_canvas == null)  
        {
            Debug.LogError("��ǰ��������û��Canvas,����ӣ�");
        }

        return gameObject_canvas;
    }

    /// <summary>
    /// ��Ŀ������л�ö�Ӧ���
    /// </summary>
    /// <typeparam name="T">��Ӧ���</typeparam>
    /// <param name="Get_Obj">Ŀ�����</param>
    /// <returns></returns>
    public T AddOrGetComponent<T>(GameObject Get_Obj) where T : Component
    {
        if (Get_Obj.GetComponent<T>() != null)
        {
            return Get_Obj.GetComponent<T>();
        }

        Debug.LogWarning($"�޷���{Get_Obj}�����ϻ��Ŀ�������");
        return null;
    }

   /// <summary>
   ///��Ŀ��Panel���������У���������������ƻ�ö�Ӧ��� 
   /// </summary>
   /// <typeparam name="T">��Ӧ���</typeparam>
   /// <param name="panel">Ŀ��Panel</param>
   /// <param name="ComponentName">����������</param>
   /// <returns></returns>
   
    public T GetOrAddSingleComponentInChild<T>(GameObject panel,string ComponentName) where T : Component 
    {
        Transform[] transforms = panel.GetComponentsInChildren<Transform>();

        foreach (Transform tra in transforms) 
        {
            if (tra.gameObject.name == ComponentName)  
            {
                return tra.gameObject.GetComponent<T>();
                break;
            }
        }

        Debug.LogWarning($"û����{panel.name}���ҵ�{ComponentName}���壡");
        return null;
    } 

}
