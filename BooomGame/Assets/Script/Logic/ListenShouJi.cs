using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListenShouJi : MonoBehaviour
{
    public GameObject thisSelf;
    public int itemID;
    // Start is called before the first frame update
    void Start()
    {
        PlayerEventManager.Instance.AddListener(delegate (string message, StuffEnum item, TriggerType type, Transform _transform)
        {
            if (message == "to touch" && (int)item == itemID)
            {
                GameObject.Destroy(thisSelf);

            }

          

        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
