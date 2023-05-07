using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Control the scene UI object push to stack or pop out from stack
/// Control the dict info for manager
/// </summary>
[System.Serializable]
public class UIManager
{
    /// <summary>
    /// uiobject���ֵ��У�KeyΪ���Ӧ��UIType�е�Name,�������ƺͶ�Ӧ���������
    /// </summary>
    public Dictionary<string,GameObject> dict_uiObject;

    /// <summary>
    /// Stored all ui object to stack
    /// </summary>
    public Stack<BasePanel> stack_ui;

    /// <summary>
    /// ��ǰ�����µ�Canvas����
    /// </summary>
    public GameObject CanvasObj;


    private static UIManager instance;
    /// <summary>
    /// ���UIManager��ʵ��
    /// </summary>
    /// <returns>UIManagerʵ��</returns>
    public static UIManager GetInstance() 
    {
        if (instance == null)
        {
            Debug.LogError("UIManager ʵ�岻���ڣ�");
            return instance;
        }
        else 
        {
            return instance;
        }
    }


    /// <summary>
    /// ʵ����UIջ�Լ�Panel�ֵ�
    /// </summary>
    /// <param name="uIManager">UIManagerʵ��</param>
    public UIManager() 
    {
        instance = this;
        dict_uiObject = new Dictionary<string, GameObject>();
        stack_ui = new Stack<BasePanel>();
    }
    
    public GameObject GetSingleObject(UIType ui_info) 
    {
        if (dict_uiObject.ContainsKey(ui_info.Name)) 
        {
            return dict_uiObject[ui_info.Name];
        }

        if (CanvasObj == null)
        {
            Debug.Log("����Canvas");
            CanvasObj = UIMethods.GetInstance().FindCanvas();
        }

        if (!dict_uiObject.ContainsKey(ui_info.Name)) 
        {
            if (CanvasObj == null)
            {
                return null;
            }
            else 
            {
                //�ӱ��ؼ���һ�����岢�ڳ�����ʵ����
                GameObject ui_obj = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>(ui_info.Path), CanvasObj.transform);
                return ui_obj;
            }
        }
        return null;
    }

    /// <summary>
    /// ag:��startpanel�Ƴ�ʱ����ʵ�������ϵ�basepanel���������Push������Ȼ��Baseʹ��UIManager�ϵ�Push����
    /// ag:startpanel����UIType����basepanel,basepanel��UIManager������ʱ����UIType�������뵽��
    /// </summary>
    /// <param name="basePanel_push">ʹ��Push��panel��UIType</param>
    public void Push(BasePanel basePanel_push) 
    {
        Debug.Log("ִ��Push");
        if (stack_ui.Count > 0) 
        {
            //��ջ�����������
            stack_ui.Peek().OnDisable();
        }

        GameObject BasePanle_pushObj = GetSingleObject(basePanel_push.uiType);
        dict_uiObject.Add(basePanel_push.uiType.Name, BasePanle_pushObj);

        //�ؼ����裺����Base Panel�ϵ�ActiveObj��Ϊ���Ǵӱ��ػ�õ����壬�Ӷ�ʹ������ڱ�����̳�ʱ����ʹ��
        basePanel_push.ActiveObj = BasePanle_pushObj;

        if (stack_ui.Count == 0)
        {
            stack_ui.Push(basePanel_push);

        }
        else 
        {
            //��ջ
            if (stack_ui.Peek().uiType.Name != basePanel_push.uiType.Name)
            {
                stack_ui.Push(basePanel_push);
            }
        }
        
        basePanel_push.OnStart();

    }


    /// <summary>
    /// ����
    /// </summary>
    /// <param name="isload">�Ƿ�Ϊ���س���</param>
    public void Pop(bool isload)
    {
        if (isload == true)  
        {
            if (stack_ui.Count > 0)  
            {
                stack_ui.Peek().OnDisable();
                stack_ui.Peek().OnDestory();
                GameObject.Destroy(dict_uiObject[stack_ui.Peek().uiType.Name]);
                dict_uiObject.Remove(stack_ui.Peek().uiType.Name);
                stack_ui.Pop();
                Pop(true);
            }
        }

        if (isload == false)  
        {
            if (stack_ui.Count > 0)
            {
                stack_ui.Peek().OnDisable();
                stack_ui.Peek().OnDestory();
                GameObject.Destroy(dict_uiObject[stack_ui.Peek().uiType.Name]);
                dict_uiObject.Remove(stack_ui.Peek().uiType.Name);
                stack_ui.Pop();

                if (stack_ui.Count > 0)
                {
                    stack_ui.Peek().OnEnable();
                }

            }
        }
        
    }
}
