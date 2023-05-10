using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackpackManager : MonoBehaviour
{
    public static BackpackManager Instance { get; private set; }

    public BackpackUIView BackpackUIView { get; set; }
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(Instance);

        CreateBackpackUI();
        LoadBooomGameStuffList();
    }

    [HideInInspector]
    public List<string> backpackItemsIDList = new List<string>();

    private void CreateBackpackUI()
    {
        GameObject temp = Resources.Load<GameObject>("Backpack");
        BackpackUIView = Instantiate(temp, transform).GetComponentInChildren<BackpackUIView>();
        BackpackUIView.getItemIDList = () => { return backpackItemsIDList; };
    }

    private void LoadBooomGameStuffList()
    {
        GameManager.StuffList = Resources.Load<StuffList>("BooomGameStuffList");
    }

}
