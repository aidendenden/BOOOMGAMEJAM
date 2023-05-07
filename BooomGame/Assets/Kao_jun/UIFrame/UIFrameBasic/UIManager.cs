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
    /// uiobject的字典中，Key为其对应的UIType中的Name,将其名称和对应的物体绑定上
    /// </summary>
    public Dictionary<string,GameObject> dict_uiObject;

    /// <summary>
    /// Stored all ui object to stack
    /// </summary>
    public Stack<BasePanel> stack_ui;

    /// <summary>
    /// 当前场景下的Canvas物体
    /// </summary>
    public GameObject CanvasObj;


    private static UIManager instance;
    /// <summary>
    /// 获得UIManager的实体
    /// </summary>
    /// <returns>UIManager实体</returns>
    public static UIManager GetInstance() 
    {
        if (instance == null)
        {
            Debug.LogError("UIManager 实体不存在！");
            return instance;
        }
        else 
        {
            return instance;
        }
    }


    /// <summary>
    /// 实例化UI栈以及Panel字典
    /// </summary>
    /// <param name="uIManager">UIManager实体</param>
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
            Debug.Log("加载Canvas");
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
                //从本地加载一个物体并在场景中实例化
                GameObject ui_obj = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>(ui_info.Path), CanvasObj.transform);
                return ui_obj;
            }
        }
        return null;
    }

    /// <summary>
    /// ag:当startpanel推出时，其实用其身上的basepanel方法上面的Push方法，然后Base使用UIManager上的Push方法
    /// ag:startpanel将其UIType传入basepanel,basepanel和UIManager交互的时候将其UIType参数传入到此
    /// </summary>
    /// <param name="basePanel_push">使用Push的panel的UIType</param>
    public void Push(BasePanel basePanel_push) 
    {
        Debug.Log("执行Push");
        if (stack_ui.Count > 0) 
        {
            //将栈顶的物体禁用
            stack_ui.Peek().OnDisable();
        }

        GameObject BasePanle_pushObj = GetSingleObject(basePanel_push.uiType);
        dict_uiObject.Add(basePanel_push.uiType.Name, BasePanle_pushObj);

        //关键步骤：将此Base Panel上的ActiveObj设为我们从本地获得的物体，从而使其可以在被子类继承时进行使用
        basePanel_push.ActiveObj = BasePanle_pushObj;

        if (stack_ui.Count == 0)
        {
            stack_ui.Push(basePanel_push);

        }
        else 
        {
            //入栈
            if (stack_ui.Peek().uiType.Name != basePanel_push.uiType.Name)
            {
                stack_ui.Push(basePanel_push);
            }
        }
        
        basePanel_push.OnStart();

    }


    /// <summary>
    /// 弹出
    /// </summary>
    /// <param name="isload">是否为加载场景</param>
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
