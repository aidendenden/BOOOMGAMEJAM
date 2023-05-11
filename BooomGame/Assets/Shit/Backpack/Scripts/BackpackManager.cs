using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackpackManager : MonoBehaviour
{
    public static BackpackManager Instance { get; private set; }

    #region UI�ڲ�����
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

    #region �ⲿ����
    /// <summary>
    /// ����Ʒ�Ž�����
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
    /// ��������Ʒ���
    /// </summary>
    /// <param name="stuffEnum"></param>
    public void OnBackpackItemViewClick(StuffEnum stuffEnum)
    {
        Debug.Log(stuffEnum + "--OnClicked!!!");
        switch (stuffEnum)
        {
            case StuffEnum.Null:
                break;
            case StuffEnum.�������߱�:
                break;
            case StuffEnum.Ƥ������:
                break;
            case StuffEnum.����:
                break;
            case StuffEnum.����ĵ���:
                break;
            case StuffEnum.��:
                break;
            case StuffEnum.ů��:
                break;
            case StuffEnum.�������õĵ����:
                break;
            case StuffEnum.̨��:
                break;
            case StuffEnum.����:
                break;
            case StuffEnum.ikun����:
                break;
            case StuffEnum.������Ĺ���ʳ:
                break;
            case StuffEnum.�ź������:
                break;
            case StuffEnum.������:
                break;
            case StuffEnum.����ͷ:
                break;
            case StuffEnum.Exit:
                break;
            case StuffEnum.������:
                break;
            case StuffEnum.��·��:
                break;
            case StuffEnum.�����Զ���ˮ��:
                break;
            case StuffEnum.����:
                break;
            case StuffEnum.ů��Ƭ������:
                break;
            case StuffEnum.���²���:
                break;
            case StuffEnum.������:
                break;
            case StuffEnum.����:
                break;
            default:
                break;
        }
    }
    #endregion

}
