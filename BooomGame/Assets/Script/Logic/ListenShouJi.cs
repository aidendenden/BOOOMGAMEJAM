using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListenShouJi : MonoBehaviour
{
    public GameObject thisSelf;
    public int itemID;
    Transform Mao;
    // Start is called before the first frame update
    void Start()
    {
        PlayerEventManager.Instance.AddListener(delegate (string message, StuffEnum item, TriggerType type, Transform _transform)
        {
            if (message == "to touch" && (int)item == itemID)
            {
                if (itemID==21)
                {
                    Mao = GameObject.FindGameObjectWithTag("Mao").transform;
                    GameManager.ChangeAlertnessValue(1000, Mao);
                    GameObject.Destroy(thisSelf);
                }
                else {
                    GameObject.Destroy(thisSelf);
                }
                

            }

          

        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
