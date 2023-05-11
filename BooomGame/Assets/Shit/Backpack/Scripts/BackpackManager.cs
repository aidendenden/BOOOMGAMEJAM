using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackpackManager : MonoBehaviour
{
    public static BackpackManager Instance { get; private set; }

    #region UI内部代码
    private BackpackUIView BackpackUIView { get; set; }
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(Instance);

        CreateBackpackUI();
    }

    private List<StuffEnum> backpackItemsIDList = new List<StuffEnum>();

    private void CreateBackpackUI()
    {
        GameObject temp = Resources.Load<GameObject>("Backpack");
        BackpackUIView = Instantiate(temp, transform).GetComponentInChildren<BackpackUIView>();
        BackpackUIView.getItemIDList = () => { return backpackItemsIDList; };
    }
    #endregion

    #region 外部调用
    /// <summary>
    /// 将物品放进背包
    /// </summary>
    /// <param name="stuffEnum"></param>
    public void PushStuffInBackPack(StuffEnum stuffEnum)
    {
        if (!(backpackItemsIDList.Count >= 4))
        {
            backpackItemsIDList.Add(stuffEnum);
        }
        
    }
    /// <summary>
    /// 背包内物品点击
    /// </summary>
    /// <param name="stuffEnum"></param>
    public void OnBackpackItemViewClick(StuffEnum stuffEnum)
    {
        Debug.Log(stuffEnum + "--OnClicked!!!");
        switch (stuffEnum)
        {
            case StuffEnum.Null:
                break;
            case StuffEnum.狗狗防疫本:
                break;
            case StuffEnum.皮带狗绳:
                break;
            case StuffEnum.座机:
                break;
            case StuffEnum.亮光的电脑:
                break;
            case StuffEnum.碗:
                break;
            case StuffEnum.暖壶:
                break;
            case StuffEnum.破破烂烂的的玩具:
                break;
            case StuffEnum.台灯:
                break;
            case StuffEnum.门锁:
                break;
            case StuffEnum.ikun海报:
                break;
            case StuffEnum.抽屉里的狗零食:
                break;
            case StuffEnum.门后的守则:
                break;
            case StuffEnum.公告栏:
                break;
            case StuffEnum.狗骨头:
                break;
            case StuffEnum.Exit:
                break;
            case StuffEnum.宿舍门:
                break;
            case StuffEnum.死路门:
                break;
            case StuffEnum.宿舍自动开水机:
                break;
            case StuffEnum.垃圾:
                break;
            case StuffEnum.暖气片和袜子:
                break;
            case StuffEnum.晾衣叉子:
                break;
            case StuffEnum.密码锁:
                break;
            case StuffEnum.盆栽:
                break;
            default:
                break;
        }
    }
    #endregion

}
