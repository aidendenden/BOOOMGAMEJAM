using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricitySwitch : MonoBehaviour
{
    public GameObject airWall ;
    private void Awake()
    {
        airWall = GameObject.Find("-kongqiqiang-");
        
        PlayerEventManager.Instance.AddListener(
            delegate(string message, StuffEnum item, TriggerType type, Transform transform1)
            {
                if (item==StuffEnum.座机 && type==TriggerType.拉闸 && airWall!=null)
                {
                    airWall.SetActive(false);
                }
            });
    }
}
